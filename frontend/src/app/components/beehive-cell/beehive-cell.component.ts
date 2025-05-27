import { Component, Output, EventEmitter, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HexagonSectorClickArgs } from '../../gameLogic/HexagonSectorClickArgs';
import { Hexagon } from '../../api-client';
import { GameFlowService } from '../../services/game-flow.service';

@Component({
  selector: 'app-beehive-cell',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './beehive.svg',
  styleUrls: ['./beehive-cell.component.scss']
})
export class BeehiveCellComponent implements OnInit, OnChanges {

  @Output() sectorClick = new EventEmitter<HexagonSectorClickArgs>();
  @Input() model: Hexagon | null = null;

  color: string = "#bbb";
  strokeColor: string = "#f00"; // default stroke color
  strokeWidth: number = 5; // default stroke width

  private higlightHex:boolean = false;

  constructor(public readonly gameFlowService: GameFlowService) {
  }

  ngOnInit(): void {
    this.color = this.model?.colorValue ?? "#bbb";
    this.strokeWidth = this.model?.affected ? 5 : 0;

    this.gameFlowService.gameFlowState$.subscribe(state =>{
      this.higlightHex = state.highlightHexagons;
      this.strokeWidth = this.model?.affected && this.higlightHex ? 5 : 0;
      this.strokeColor = state.highlighHexColor;
    });

  }

  ngOnChanges(changes: SimpleChanges): void {
    this.color = this.model?.colorValue ?? "#bbb";
     this.strokeWidth = this.model?.affected && this.higlightHex ? 5 : 0;
    // Optionally set strokeColor and strokeWidth from model if needed
  }

 onSectorClick(sectorId: number) {
    this.sectorClick.emit({ sector: sectorId, hexagon: this.model?.color ?? -1 });  
  }
}
