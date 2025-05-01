import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-player-resource-item',
  imports: [CommonModule],
  templateUrl: './player-resource-item.component.svg',
  styleUrl: './player-resource-item.component.scss'
})
export class PlayerResourceItemComponent {
  @Input() color: string = "#bbb";
}
