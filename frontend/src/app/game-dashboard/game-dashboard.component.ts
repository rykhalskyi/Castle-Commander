import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-game-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './game-dashboard.component.html',
  styleUrls: ['./game-dashboard.component.scss']
})
export class GameDashboardComponent {

  protected gameId = signal<string>('');

constructor(private readonly gameService: GameService) {
 }

  public newGameClick(): void {
    this.gameService.startNewGame().subscribe((game) => {
      this.gameId.set(game.id);
    });
  }

}
