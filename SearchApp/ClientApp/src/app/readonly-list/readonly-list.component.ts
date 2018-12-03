import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-readonly-list',
  templateUrl: './readonly-list.component.html',
  styleUrls: ['./readonly-list.component.css']
})
export class ReadonlyListComponent implements OnInit {
  @Input() slots: Slot[];
  constructor() { }

  ngOnInit() {
    var v = this.slots;
  }

}


interface Slot {
  slotDescription: string;
  slotStartTime: Date;
  slotEndTime: Date;
}
