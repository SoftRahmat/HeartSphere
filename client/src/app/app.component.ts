import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatListModule} from '@angular/material/list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatListModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  _http: HttpClient = inject(HttpClient);
  title: string = 'Heart Sphere';
  users: any = {};


  ngOnInit(): void {
    this._http.get('https://localhost:5001/api/users').subscribe({
      next: res=> { this.users = res },
      error: error => console.log(error),
      complete: () => console.log('Request completed successfully')
    })
  }
}
