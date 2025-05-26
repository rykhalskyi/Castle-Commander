import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-enemy-card',
  templateUrl: './enemy-card.component.html',
  styleUrl: './enemy-card.component.scss',
  standalone: true,
  imports: [NgIf]
})
export class EnemyCardComponent {
  @Input() enemy: any;
  @Input() enemyText: string = 'Enemy Card';
  revealed = false;

  revealCard() {
    this.revealed = true;
  }
}
