import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-message-popup',
  imports: [CommonModule],
  templateUrl: './message-popup.component.html',
  styleUrls: ['./message-popup.component.scss']
})
export class MessagePopupComponent implements OnChanges {
  @Input() message: string = '';
  isVisible: boolean = false;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['message'] && changes['message'].currentValue) {
      this.showPopup();
    }
  }

  private showPopup(): void {
    this.isVisible = true;
    // setTimeout(() => {
    //   this.isVisible = false;
    // }, 3000); // 3 seconds
  }
}