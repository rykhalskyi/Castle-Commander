import { Component, OnInit, signal } from '@angular/core';

import { HexagonSectorClickArgs } from '../gameLogic/HexagonSectorClickArgs';
import { BeehiveCellComponent } from '../beehive-cell/beehive-cell.component';
import { CastleHexagonComponent } from '../castle-hexagon/castle-hexagon.component';
import { Game, Hexagon } from '../api-client';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-playfield',
  templateUrl: './playfield.component.html',
    imports: [BeehiveCellComponent, CastleHexagonComponent],
  styleUrls: ['./playfield.component.scss']
})
export class PlayfieldComponent implements OnInit {
    protected hexagons = signal<Hexagon[]>([]);
    protected showHexagons = signal<boolean>(false);

    constructor(private readonly gameService:GameService
    ) {
    }

    ngOnInit(): void {
        
        this.gameService.activeGame.subscribe((game) => {
          this.hexagons.set(game?.castle?.hexagons ?? []);
          this.showHexagons.set((game?.castle?.hexagons?.length ?? 0) > 0);
        });
    }

  // Add component logic here
  onClick(args:HexagonSectorClickArgs)
  {
    console.log("Click "+args.hexagon + ":"+args.sector);
  }
}