import { Component, OnInit, signal } from '@angular/core';
import { HexagonSectorClickArgs } from '../gameLogic/HexagonSectorClickArgs';
import { CastleHexagonComponent } from '../castle-hexagon/castle-hexagon.component';
import { FacilitySize, Game, Hexagon } from '../api-client';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-playfield',
  templateUrl: './playfield.component.html',
    imports: [CastleHexagonComponent],
  styleUrls: ['./playfield.component.scss']
})
export class PlayfieldComponent implements OnInit {
    protected hexagons = signal<Hexagon[]>([]);
    protected showHexagons = signal<boolean>(false);
    private game: Game | null = null;

    constructor(private readonly gameService:GameService
    ) {
    }

    ngOnInit(): void {
        
        this.gameService.activeGame.subscribe((game) => {
          this.hexagons.set(game?.castle?.hexagons ?? []);
          this.showHexagons.set((game?.castle?.hexagons?.length ?? 0) > 0);
          this.game = game;
        });
    }

  // Add component logic here
  onClick(args:HexagonSectorClickArgs)
  {
    if (this.game === null) {
      return;
    }
    this.gameService.addFacility(this.game, args.hexagon, args.sector, FacilitySize._3)
    console.log("Click "+args.hexagon + ":"+args.sector);
  }
}