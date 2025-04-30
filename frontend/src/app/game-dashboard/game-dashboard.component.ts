import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameService } from '../services/game.service';
import { Game, Player } from '../api-client';
import { PlayerListComponent } from '../player-list/player-list.component';

@Component({
  selector: 'app-game-dashboard',
  standalone: true,
  imports: [CommonModule, PlayerListComponent],
  templateUrl: './game-dashboard.component.html',
  styleUrls: ['./game-dashboard.component.scss']
})
export class GameDashboardComponent {

  protected gameId = signal<string>('');
  protected players = signal<Player[]>([]);
  protected game = signal<Game | null>(null);

constructor(
  private readonly gameService: GameService) { }

  public async newGameClick(): Promise<void> {
    const game =  await this.gameService.client.startnew();
    this.gameId.set(game.id!);
    this.players.set(game.players!);
    this.game.set(game);
  }

  public async makeTurnClick(): Promise<void> {
    if (this.game !== null) {
      const game = await this.gameService.client.nextturn(this.game()!);
      this.game.set(game);
    }
  }
}
