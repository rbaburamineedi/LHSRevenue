import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-meeting-scheduler',
  templateUrl: './meeting-scheduler.component.html',
  styleUrls: ['./meeting-scheduler.component.css']
})
export class MeetingSchedulerComponent implements OnInit {
  public slots: Array<Slot>;
  public baseURL;
  public slot = {
    slotDescription: '',
    slotDate: '',
    slotDuration: 0
  };
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseURL = baseUrl;
    this.fetchData();
  }

  fetchData() {
    this.http.get<Slot[]>(this.baseURL + 'api/SampleData/Slots').subscribe(result => {
      this.slots = result;
    }, error => console.error(error));
  }

  createMeeting() {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    this.http.post(this.baseURL + 'api/SampleData/SaveSlot', this.slot, { headers }).subscribe(result => {
      if (result == 1) {
        alert('Success!');
        this.fetchData();
      }
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

interface Slot {
  slotDescription: string;
  slotStartTime: Date;
  slotEndTime: Date;
}
