import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Client, Game } from '../api-client';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  public client: Client = new Client(`${environment.apiUrl}`);
  public activeGame: BehaviorSubject<Game | null> = new BehaviorSubject<Game | null>(null);

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

  public async addSmallFacility(game: Game, hexagon:number, sector: number): Promise<Game> {
    const updatedGame = await this.client.addfacility(hexagon, sector, 1, game);
    this.activeGame.next(updatedGame);
    return updatedGame;
  }
  constructor() { } 
 
}
