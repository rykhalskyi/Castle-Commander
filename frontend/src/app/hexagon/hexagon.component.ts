import { Component, EventEmitter, Input, OnInit, Output, OnChanges } from '@angular/core';
import { Hexagon } from '../gameLogic/hexagon';
import { HexagonSectorClickArgs } from '../gameLogic/HexagonSectorClickArgs';
import { CommonModule } from '@angular/common';


export const SECTORNUM = 6;

@Component({
  selector: 'app-hexagon',
  imports: [CommonModule],
  templateUrl: './hexagon.component.html',
  styleUrl: './hexagon.component.scss',
  
})
export class HexagonComponent implements OnInit, OnChanges {
  color: string;
  @Input() model: Hexagon | null = null;

  @Output() sectorClick: EventEmitter<any> = new EventEmitter();

  sectorFill : string[] = ["#fff","#f0f","#ff0","0ff","#fff","#f0f"];
  sectorTextFill : string[] = ["#000","#000","#000","000","#000","#000"];
  sectorValue : string[][] = [["+1","+2"],["+3","+4"],["+5","+6"],["+7","+8"],["+9","+10"],["+11","+12"]];

  value : string[] = ["+6","+16"];

  constructor() {
    this.color = this.model?.color ?? "#bbb";
    console.log("Hex constructor");
  }

  ngOnInit(): void {
  }

  ngOnChanges(): void {
    this.Refresh();
  }

  onMouseUp(sector:number) {

    var args: HexagonSectorClickArgs = {
      hexagon : this.model != null ? this.model.index : -1,
      sector : sector
    }

    this.sectorClick.emit(args);
  }

  private Refresh():void
  {
    this.Clean();

    if (this.model == null)
     return;

    if (this.model.sectors == null)
     return;

    this.color = this.model.color;

    var i = 0;
    this.model.sectors.forEach(sector => {
      if (sector != null)
      {
          this.sectorFill[i] = sector.player != null ? sector.player.color : "transparent";
          this.sectorTextFill[i] = sector.player != null ? "#000" : "transparent";
          this.sectorValue[i][0] = "+" + sector.attack;
          this.sectorValue[i][1] = "+" + sector.defence;
      }
    } );
  }

  Clean()
  {
    for (var i=0; i<SECTORNUM; i++)
    {
      this.sectorFill[i] = 'transparent';
    }

    this.sectorValue.forEach(i=> {
      i[0] = "";
      i[1] = "";
    });

    var attack = this.model == null ? SECTORNUM : SECTORNUM * this.model.defaultSectorAttack;
    var defence = this.model == null ? SECTORNUM : SECTORNUM * this.model.defaultSectorDefence;

    this.value = [attack.toString(), defence.toString() ]
  }

  printHex(hex:Hexagon)
  {
    console.log("Hex: "+ hex.color + " " + hex.defaultSectorAttack + " " + hex.defaultSectorDefence);
  }

}
