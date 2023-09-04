import { Component } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  inputValue: string = '';
  responseText: string = '';
  errorMessage: string = '';

  constructor(private http: HttpClient) {}

  showResponse() {

    const apiUrl = environment.apiUrl+'url/shorten'; // Use the environment variable

    const requestBody = {
      LongUrl: this.inputValue
    };
    this.http.post<any>(apiUrl,requestBody)
      .pipe(
        map((data) => data), // Map to the desired property
        catchError((error) => {
          console.error('API Error:', error);
          this.errorMessage = error.error?.message;
          return []; // Return a default value or handle the error as needed
        })
      )
      .subscribe((responseText) => {
          this.responseText = responseText.data;
      });
  }

  handleClick() {
    // Make a GET request to trigger the backend call
    this.http.get(this.responseText)
      .pipe(
      map((data) => data), // Map to the desired property
      catchError((error) => {
        console.error('API Error:', error);
        return []; // Return a default value or handle the error as needed
      })
    ).subscribe((resp:any) => {
        const url = resp.data;
        window.open(url, '_blank');
      }
    );
  }

  textInput() {
    this.responseText = '';
    this.errorMessage = '';
  }
}
