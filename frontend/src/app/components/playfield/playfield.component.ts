import { Component, Input, OnInit, signal } from '@angular/core';
import { HexagonSectorClickArgs } from '../../gameLogic/HexagonSectorClickArgs';
import { CastleHexagonComponent } from '../castle-hexagon/castle-hexagon.component';
import { FacilitySize, Game, Hexagon } from '../../api-client';
import { GameService } from '../../services/game.service';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

@UntilDestroy()
@Component({
  selector: 'app-playfield',
  templateUrl: './playfield.component.html',
    imports: [CastleHexagonComponent],
  styleUrls: ['./playfield.component.scss']
})
export class PlayfieldComponent implements OnInit {
    private buttonsDisabled: boolean = false;

    protected hexagons = signal<Hexagon[]>([]);
    protected showHexagons = signal<boolean>(false);
    protected showScores = signal<boolean>(true);

    private game: Game | null = null;

    constructor(private readonly gameService:GameService
    ) {
    }

    ngOnInit(): void {
        this.buttonsDisabled = this.game?.playerId !== this.game?.players![this.game.currentPlayer!].id;
        
        this.gameService.activeGame
        .pipe(untilDestroyed(this))
        .subscribe((game) => {
          this.hexagons.set(game?.castle?.hexagons ?? []);
          this.showHexagons.set((game?.castle?.hexagons?.length ?? 0) > 0);
          this.game = game;
          this.buttonsDisabled = this.game?.playerId !== this.game?.players![this.game.currentPlayer!].id;
        });
    }

  // Add component logic here
  async onClick(args:HexagonSectorClickArgs):Promise<void>
  {
    if (this.game === null || this.buttonsDisabled) return;
    
    await this.buildOrRepair(args);

    await this.towerAttack(args);

    console.log("Click "+args.hexagon + ":"+args.sector);
  }

  private async towerAttack(args:HexagonSectorClickArgs): Promise<void>
  {
      if (this.game!.currentTurn !== 3) return;

      await this.gameService.towerAttack(this.game!, args.hexagon);

  }

  private async buildOrRepair(args:HexagonSectorClickArgs):Promise<void>
  {
    if (this.game!.currentTurn !== 2) return;

    if (this.gameService.selectedFacilitySize !== null
          && !this.gameService.rapair) 
    {
        await this.gameService.addFacility(this.game!, args.hexagon, args.sector, this.gameService.selectedFacilitySize);
    }
    else 
    {
        await this.gameService.repairFacility(this.game!, args.hexagon, args.sector)
    }
  }
}