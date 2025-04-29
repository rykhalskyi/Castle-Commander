import { Component } from '@angular/core';
import { PlayfieldComponent } from './playfield/playfield.component';
import { GameDashboardComponent } from './game-dashboard/game-dashboard.component';
import { GameService } from './services/game.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  imports: [PlayfieldComponent, GameDashboardComponent, ],
  standalone: true,
  providers: [ GameService],
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'castle-commander';
}
