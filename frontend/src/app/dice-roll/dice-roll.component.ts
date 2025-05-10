import { Component } from '@angular/core';

@Component({
  selector: 'app-dice-roll',
  templateUrl: './dice-roll.component.html',
  styleUrls: ['./dice-roll.component.scss']
})
export class DiceRollComponent {
  dice1: number = 1;
  dice2: number = 1;
  rolling: boolean = false;

  rollDice() {
    if (this.rolling) return;
    this.rolling = true;
    const rollInterval = setInterval(() => {
      this.dice1 = Math.floor(Math.random() * 6) + 1;
      this.dice2 = Math.floor(Math.random() * 6) + 1;
    }, 100);

    setTimeout(() => {
      clearInterval(rollInterval);
      this.rolling = false;
    }, 500);
  }
}
