import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Player } from '../../api-client';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-player-list',
  imports: [CommonModule, CommonModule],
  templateUrl: './player-list.component.html',
  styleUrl: './player-list.component.scss'
})
export class PlayerListComponent implements OnChanges {

  @Input() players: Player[] | null = null;
  @Input() playerId: string = '';

  ngOnChanges(changes: SimpleChanges): void {
    console.log("PlayerListComponent ngOnChanges", changes);
  }  
}
