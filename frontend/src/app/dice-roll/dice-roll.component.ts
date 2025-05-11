import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Dice } from '../api-client';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dice-roll',
  imports: [CommonModule],
  templateUrl: './dice-roll.component.html',
  styleUrls: ['./dice-roll.component.scss']
})
export class DiceRollComponent implements OnInit {
  @Input() dice: Dice | null = null;

  dice1: number = 1;
  dice2: string = '?';
  dice1Color: string = '#FF0000';
  rolling: boolean = false;

  ngOnInit(): void {
    console.log('onInit', this.rolling);
    if (this.dice) {
      this.rollDice();
    }
  }

  rollDice() {
    if (this.rolling) return;
    this.rolling = true;
    const rollInterval = setInterval(() => {
      this.dice1 = Math.floor(Math.random() * 6) + 1;
      this.dice2 = Math.floor(Math.random() * 6).toString();
      this.dice1Color = this.dice?.resourceDiceColor ?? '#CCCCCC'
    }, 100);

    setTimeout(() => {
      clearInterval(rollInterval);
      this.rolling = false;
      this.dice1 = this.dice!.resorceDice!;
      this.dice2 = this.dice!.bonusDice!;
    }, 500);
  }
}
