import { Component } from '@angular/core';
import { PlayfieldComponent } from './components/playfield/playfield.component';
import { GameDashboardComponent } from './components/game-dashboard/game-dashboard.component';
import { GameService } from './services/game.service';
import { MessagePopupComponent } from './components/message-popup/message-popup.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  imports: [PlayfieldComponent, GameDashboardComponent, MessagePopupComponent],
  standalone: true,
  providers: [ GameService],
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'castle-commander';
}
