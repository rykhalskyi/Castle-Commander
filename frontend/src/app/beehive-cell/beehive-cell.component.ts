import { Component, Output, EventEmitter, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HexagonSectorClickArgs } from '../gameLogic/HexagonSectorClickArgs';
import { Hexagon } from '../api-client';


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

  ngOnInit(): void {
    this.color = this.model?.colorValue ?? "#bbb";
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.color = this.model?.colorValue ?? "#bbb";
  }

 onSectorClick(sectorId: number) {
    this.sectorClick.emit({ sector: sectorId, hexagon: this.model?.color ?? -1 });  
  }
}
