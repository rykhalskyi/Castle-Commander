import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HexagonComponent } from './hexagon/hexagon.component';
import { CommonModule } from '@angular/common';
import { PlayfieldComponent } from './playfield/playfield.component';
import { BeehiveCellComponent } from './beehive-cell/beehive-cell.component';

@NgModule({
  declarations: [
    AppComponent,
    HexagonComponent,
    PlayfieldComponent,
    BeehiveCellComponent
  ],
  imports: [
    BrowserModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }