import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameService } from '../services/game.service';
import { Game, Player } from '../api-client';
import { PlayerListComponent } from '../player-list/player-list.component';
import { ChooseFacilityComponent } from '../choose-facility/choose-facility.component';
import { DiceRollComponent } from '../dice-roll/dice-roll.component';
import { ResourceExchangeComponent } from '../resource-exchange/resource-exchange.component';

@Component({
  selector: 'app-game-dashboard',
  standalone: true,
  imports: [CommonModule, 
    PlayerListComponent, 
    ChooseFacilityComponent, 
    DiceRollComponent, 
    ResourceExchangeComponent
  ],
  templateUrl: './game-dashboard.component.html',
  styleUrls: ['./game-dashboard.component.scss']
})
export class GameDashboardComponent implements OnInit {

  protected gameId = signal<string>('');
  protected players = signal<Player[]>([]);
  protected game = signal<Game | null>(null);
  protected currentPlayer = signal<Player | null>(null);

constructor(
  private readonly gameService: GameService) { }

  ngOnInit(): void {
    this.gameService.activeGame.subscribe((game) => {
      if (game) {
        this.gameId.set(game.id!);
        this.players.set(game.players!);
        this.game.set(game);
        this.currentPlayer.set(game.players![game.currentPlayer!]);

        if (game.log) console.log('** Game Log:', game.log);
        
      }
    });
  }

  public async newGameClick(): Promise<void> {
    const game =  await this.gameService.startNewGame();
  }

  public async makeTurnClick(): Promise<void> {
    if (this.game !== null) {
      const game = await this.gameService.nextTurn(this.game()!);
    }
  }
}

