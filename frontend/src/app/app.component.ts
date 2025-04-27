import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HexagonComponent } from './hexagon/hexagon.component';
import { PlayfieldComponent } from './playfield/playfield.component';
import { BeehiveCellComponent } from './beehive-cell/beehive-cell.component';



@Component({
  selector: 'app-root',
  imports: [PlayfieldComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'castle-commander';
}
