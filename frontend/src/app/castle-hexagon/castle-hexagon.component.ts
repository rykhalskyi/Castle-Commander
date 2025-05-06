import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BeehiveCellComponent } from '../beehive-cell/beehive-cell.component';
import { SmallFacilitiesComponent } from '../small-facilities/small-facilities.component';
import { HexagonSectorClickArgs } from '../gameLogic/HexagonSectorClickArgs';
import { CommonModule } from '@angular/common';
import { Hexagon } from '../api-client';
import { MediumFacilitiesComponent } from '../medium-facilities/medium-facilities.component';

@Component({
  selector: 'app-castle-hexagon',
  imports: [BeehiveCellComponent, SmallFacilitiesComponent, CommonModule, MediumFacilitiesComponent],
  templateUrl: './castle-hexagon.component.html',
  styleUrl: './castle-hexagon.component.scss'
})
export class CastleHexagonComponent {

  @Output() sectorClick = new EventEmitter<HexagonSectorClickArgs>();
  @Input() model: Hexagon | null = null;

  protected onSectorClick(args:HexagonSectorClickArgs) {
    this.sectorClick.emit(args);
  }

}
