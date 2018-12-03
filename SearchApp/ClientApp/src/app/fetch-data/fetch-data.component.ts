import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public posts: Array<Post>;
  public baseURL;
  public pos = {
    title: 't7',
    blogId: 1,
    content: 'c8'
  };
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseURL = baseUrl;
    http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  insert() {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    this.http.post(this.baseURL + 'api/SampleData/SavePost', this.pos, { headers }).subscribe(result => {
      if (result == 1) {
        alert('Success!');
      }
    }, error => console.error(error));
  }

  fetch() {
    this.http.get<Post[]>(this.baseURL + 'api/SampleData/Posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }

}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

interface Post {
  postId: number;
  title: string;
  content: string;
  blogId: number;
}
