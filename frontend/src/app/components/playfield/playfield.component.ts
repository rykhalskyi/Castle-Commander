import { Component, OnInit, signal } from '@angular/core';
import { HexagonSectorClickArgs } from '../../gameLogic/HexagonSectorClickArgs';
import { CastleHexagonComponent } from '../castle-hexagon/castle-hexagon.component';
import { FacilitySize, Game, Hexagon } from '../../api-client';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-playfield',
  templateUrl: './playfield.component.html',
    imports: [CastleHexagonComponent],
  styleUrls: ['./playfield.component.scss']
})
export class PlayfieldComponent implements OnInit {
    protected hexagons = signal<Hexagon[]>([]);
    protected showHexagons = signal<boolean>(false);
    protected showScores = signal<boolean>(false);

    private game: Game | null = null;

    constructor(private readonly gameService:GameService
    ) {
    }

    ngOnInit(): void {
        
        this.gameService.activeGame.subscribe((game) => {
          this.hexagons.set(game?.castle?.hexagons ?? []);
          this.showHexagons.set((game?.castle?.hexagons?.length ?? 0) > 0);
          this.game = game;
          this.showScores.set(this.game?.currentTurn === 3);
        });
    }

  // Add component logic here
  onClick(args:HexagonSectorClickArgs)
  {
    if (this.game === null 
      || this.game.currentTurn !== 2
      || this.gameService.selectedFacilitySize === null) {
      return;
    }
    this.gameService.addFacility(this.game, args.hexagon, args.sector, this.gameService.selectedFacilitySize);
    console.log("Click "+args.hexagon + ":"+args.sector);
  }
}