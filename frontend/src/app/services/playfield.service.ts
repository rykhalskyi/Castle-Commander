import { Injectable } from '@angular/core';
import { Hexagon } from '../gameLogic/hexagon'; // Adjust the path as needed

@Injectable({
  providedIn: 'root'
})
export class PlayfieldService {

  constructor() { }

  // Added 7 hex colors to the colors array
  private colors: string[] = ['#FF0000', '#FFA500', '#dbde33', '#008000', '#33c1de', '#000080', '#73018f'];

  private red : Hexagon = {
    index : 0,
    color : this.colors[0],
    sectors : [],
    defaultSectorAttack : 1,
    defaultSectorDefence : 1
   };

   private orange : Hexagon = {
     index : 1,
     color : this.colors[1],
     sectors : [],
     defaultSectorAttack : 1,
     defaultSectorDefence : 1
    };

    private yellow : Hexagon = {
     index : 2,
     color : this.colors[2],
     sectors : [],
     defaultSectorAttack : 1,
     defaultSectorDefence : 1
    };

    private green : Hexagon = {
     index : 3,
     color : this.colors[3],
     sectors : [],
     defaultSectorAttack : 1,
     defaultSectorDefence : 1
    };

    private blue : Hexagon = {
     index : 4,
     color : this.colors[4],
     sectors : [],
     defaultSectorAttack : 1,
     defaultSectorDefence : 1
    };

    private navy : Hexagon = {
     index : 5,
     color : this.colors[5],
     sectors : [],
     defaultSectorAttack : 1,
     defaultSectorDefence : 1
    };

    private violet : Hexagon = {
     index : 6,
     color : this.colors[6],
     sectors : [],
     defaultSectorAttack : 1,
     defaultSectorDefence : 1
    };

    public getHexagons(): Hexagon[] {
      return [this.red, this.orange, this.yellow, this.green, this.blue, this.navy, this.violet]; 
    }

}
