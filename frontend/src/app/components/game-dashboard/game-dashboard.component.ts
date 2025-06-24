import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameService } from '../../services/game.service';
import { Game, Player } from '../../api-client';
import { PlayerListComponent } from '../player-list/player-list.component';
import { ChooseFacilityComponent } from '../choose-facility/choose-facility.component';
import { DiceRollComponent } from '../dice-roll/dice-roll.component';
import { ResourceExchangeComponent } from '../resource-exchange/resource-exchange.component';
import { EnemyCardComponent } from '../enemy-card/enemy-card.component';
import { GameFlowService, IGameFlowState } from '../../services/game-flow.service';
import { FormsModule } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { ChoosePlayerComponent } from "../choose-player/choose-player.component";

@UntilDestroy()
@Component({
  selector: 'app-game-dashboard',
  standalone: true,
  imports: [CommonModule,
    PlayerListComponent,
    ChooseFacilityComponent,
    DiceRollComponent,
    ResourceExchangeComponent,
    EnemyCardComponent,
    FormsModule, ChoosePlayerComponent],
  templateUrl: './game-dashboard.component.html',
  styleUrls: ['./game-dashboard.component.scss']
})
export class GameDashboardComponent implements OnInit {
  protected players = signal<Player[]>([]);
  protected game = signal<Game | null>(null);
  protected currentPlayer = signal<Player | null>(null);
  protected newGame = true;
  protected gameId = 'No Game ID';

constructor(
  private readonly gameService: GameService,
private readonly gameFlowService: GameFlowService) { }

  ngOnInit(): void {
    this.gameService.activeGame
    .pipe(untilDestroyed(this))
    .subscribe((game) => {
      if (game) {
        this.gameId = game.id!;
        this.players.set(game.players!);
        this.game.set(game);
        this.currentPlayer.set(game.players![game.currentPlayer!]);
        
        if (game.currentTurn == 0 || game.currentTurn == 4)
        {
          this.gameFlowService.setDefaultState();
        }

        if (game.log) console.log('** Game Log:', game.log);
        
      }
    });
  }

  public async newGameClick(): Promise<void> {
    if (this.newGame)
      {
        const game =  await this.gameService.startNewGame();
      }
    else await this.gameService.getGame(this.gameId);
  }

  public async makeTurnClick(): Promise<void> {
    if (this.game !== null) {
      const game = await this.gameService.nextTurn(this.game()!);
    }
  }

  public onGameIdInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.gameId = input.value;
    if (this.game() !== null) {
      this.game()!.id = input.value;
    }
  }
}

