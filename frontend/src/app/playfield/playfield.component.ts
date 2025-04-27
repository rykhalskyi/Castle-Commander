import { Component, OnInit, signal } from '@angular/core';
import { Hexagon } from '../gameLogic/hexagon';
import { HexagonSectorClickArgs } from '../gameLogic/HexagonSectorClickArgs';
import { HexagonComponent } from '../hexagon/hexagon.component';
import { PlayfieldService } from '../services/playfield.service';
import { BeehiveCellComponent } from '../beehive-cell/beehive-cell.component';

@Component({
  selector: 'app-playfield',
  templateUrl: './playfield.component.html',
    imports: [BeehiveCellComponent],
  styleUrls: ['./playfield.component.scss']
})
export class PlayfieldComponent implements OnInit {
    protected hexagons = signal<Hexagon[]>([]);

    constructor(private readonly playfiledService:PlayfieldService) {
    }

    ngOnInit(): void {
        this.hexagons.set(this.playfiledService.getHexagons());
    }

  // Add component logic here
  onClick(args:HexagonSectorClickArgs)
  {
    console.log("Click "+args.hexagon + ":"+args.sector);
  }
}