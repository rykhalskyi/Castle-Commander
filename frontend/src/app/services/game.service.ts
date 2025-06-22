import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AddFacilityInput, Client, ExchangeItem, BuyItemInput, FacilitySize, Game, ExchangeItemInput, RepairFacilityInput, BuyOnMarketInput, BuyCoinsOnMarketInput, TowerAttackInput } from '../api-client';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  public client: Client = new Client(`${environment.apiUrl}`);
  public activeGame: BehaviorSubject<Game | null> = new BehaviorSubject<Game | null>(null);
  public selectedFacilitySize: FacilitySize | null = null;
  public rapair: boolean = false;
  private playerId: string = "";

  public startNewGame: () => Promise<Game> = async () => {
    const game = await this.client.startnew();
    this.activeGame.next(game);
    return game;
  }

  public async getGame(gameId: string): Promise<Game> {
    const game = await this.client.getgame(gameId);
    this.activeGame.next(game);
    return game;
  }

  public async joinGame(gameId: string): Promise<Game>{
    const game = await this.client.join(gameId);
    this.activeGame.next(game);
    return game;
  }

  public nextTurn: (game: Game) => Promise<Game> = async (game: Game) => {
    const updatedGame = await this.client.nextturn(game);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async addFacility(game: Game, hexagon:number, sector: number, size: FacilitySize): Promise<Game> {
    const updatedGame = await this.client.addfacility({
      hexagon : hexagon,
      startSector : sector,
      size : size, 
      gameId: game.id,
      playerId: game.playerId} as AddFacilityInput);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async buy(gameId: string, playerId:string, item: ExchangeItem): Promise<Game> {
    const updatedGame = await this.client.buy({
      gameId: gameId,
      playerId: playerId,
      item: item,
      number: 1
    } as BuyItemInput);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async exchange(gameId: string, playerId:string, otherPlayer: string, resource:number, otherResource: number): Promise<Game> {
    const updatedGame = await this.client.exchange({
      gameId: gameId,
      playerId: playerId,
      otherPlayer: otherPlayer,
      playerResource: resource,
      otherResource: otherResource
    } as ExchangeItemInput);

    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async repairFacility(game: Game, hexagon: number, sector: number){
    const updatedGame = await this.client.repairfacility({
      gameId: game.id,
      playerId: game.playerId,
      hexagon: hexagon,
      sector: sector
    } as RepairFacilityInput);

    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async buyOnMarket(gameId: string, playerId:string, resourceTosell: number, resourceToBuy: number): Promise<Game>
  {
     const updatedGame = await this.client.buyonmarket({
      gameId: gameId,
      playerId: playerId,
      resourceToBuy: resourceToBuy,
      resourceToSell: resourceTosell
     } as BuyOnMarketInput);

     this.activeGame.next(updatedGame);
     return updatedGame;
  }

  public async towerAttack(game: Game, hex:number): Promise<Game>
  {
    const updatedGame = await this.client.towerattack({
      gameId : game.id,
      playerId: this.playerId,
      hexagon: hex
    } as TowerAttackInput);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async buyCoinOnMarket(gameId: string, item: ExchangeItem, resourcesToSell:number[]): Promise<Game>
    {
        const updatedGame = await this.client.buycoinsonmarket({
          gameId: gameId,
          item: item,
          resources: resourcesToSell
        } as BuyCoinsOnMarketInput);

        this.activeGame.next(updatedGame);
        return updatedGame;
    }
  

  public subscribeToGameEvents(
    gameId: string,
    onEvent: (data: { gameId: string; playerId: string }) => void
  ): EventSource {
    const url = `${this.client["baseUrl"]}/api/game/events/${encodeURIComponent(gameId)}`;
    const eventSource = new EventSource(url);
    eventSource.onmessage = (event) => {
      try {
        // event.data is a string like: { "gameId": "...", "playerId": "..." }
        const data = JSON.parse(event.data);
        onEvent(data);
      } catch (e) {
        console.error("Failed to parse SSE event data", e, event.data);
      }
    };
    eventSource.onerror = (err) => {
      console.error("SSE connection error", err);
    };
    return eventSource;
  }

  constructor() { } 
 
}
