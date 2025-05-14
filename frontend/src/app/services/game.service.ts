import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AddFacilityInput, Client, ExchangeItem, ExchangeItemInput, FacilitySize, Game } from '../api-client';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  public client: Client = new Client(`${environment.apiUrl}`);
  public activeGame: BehaviorSubject<Game | null> = new BehaviorSubject<Game | null>(null);
  public selectedFacilitySize: FacilitySize | null = null;

  public startNewGame: () => Promise<Game> = async () => {
    const game = await this.client.startnew();
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
      inputGame: game,
      playerId: game.currentPlayer} as AddFacilityInput);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }

  public async exchange(gameId: string, item: ExchangeItem): Promise<Game> {
    const updatedGame = await this.client.exchange({
      gameId: gameId,
      item: item,
      number: 1
    } as ExchangeItemInput);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }
  constructor() { } 
 
}
