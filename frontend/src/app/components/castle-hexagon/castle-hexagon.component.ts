import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { BeehiveCellComponent } from '../beehive-cell/beehive-cell.component';
import { SmallFacilitiesComponent } from '../small-facilities/small-facilities.component';
import { MediumFacilitiesComponent } from '../medium-facilities/medium-facilities.component';
import { LargeFacilitiesComponent } from '../large-facilities/large-facilities.component';
import { HexagonSectorClickArgs } from '../../gameLogic/HexagonSectorClickArgs';
import { CommonModule } from '@angular/common';
import { Hexagon } from '../../api-client';

@Component({
  selector: 'app-castle-hexagon',
  imports: [BeehiveCellComponent, SmallFacilitiesComponent, CommonModule, MediumFacilitiesComponent, LargeFacilitiesComponent],
  templateUrl: './castle-hexagon.component.html',
  styleUrl: './castle-hexagon.component.scss'
})
export class CastleHexagonComponent implements OnChanges, OnInit {
  
  @Output() sectorClick = new EventEmitter<HexagonSectorClickArgs>();
  @Input() model: Hexagon | null = null;
  @Input() showScores: boolean = false;

  protected defenceScore: number[] = [0,0,0,0,0,0];
  protected score: string = '';

  ngOnChanges(changes: SimpleChanges): void {
    this.defenceScore = this.model!.sectors!.map(i=>i.defenceScore!);
    const sumDefenceScore = this.defenceScore.reduce((sum, val) => sum + val, 0);
    this.score = sumDefenceScore.toString();
  }

  ngOnInit(): void {
    this.defenceScore = this.model!.sectors!.map(i=>i.defenceScore!);
    const sumDefenceScore = this.defenceScore.reduce((sum, val) => sum + val, 0);
    this.score = sumDefenceScore.toString();
  }

  protected onSectorClick(args:HexagonSectorClickArgs) {
    this.sectorClick.emit(args);
  }

}
