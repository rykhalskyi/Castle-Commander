import { Component, Input, OnInit } from '@angular/core';
import { GameService } from '../../services/game.service';
import { Player } from '../../api-client';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-choose-player',
  templateUrl: './choose-player.component.html',
  imports: [CommonModule, FormsModule],
  styleUrls: ['./choose-player.component.scss']
})
export class ChoosePlayerComponent implements OnInit {
  @Input() players: Player[] = [];
  @Input() currentPlayer: number = 0;
  selectedPlayerName: string = '';

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    if (this.players && this.players.length > 0) 
    {
        this.selectedPlayerName = this.players[this.currentPlayer].name!;
        this.gameService.selectedPlayerId = this.players[this.currentPlayer].id!;
    }
    // console.log('players', this.gameService.selectedPlayerId, this.selectedPlayerName);
  }

  onPlayerChange(event: Event): void {
    const name = (event.target as HTMLSelectElement).value;
    const player = this.players.find(p => p.name === name);
    if (player) {
      this.gameService.selectedPlayerId = player.id!;
    }
  }
}
