import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  _http: HttpClient = inject(HttpClient);
  users: any = {};

  ngOnInit(): void {
    this.getUsers();
  }
  registerToggle(): void {
    this.registerMode = !this.registerMode;
  }

  getUsers(): any { 
    this._http.get('https://localhost:5001/api/users').subscribe({
      next: res=> { this.users = res },
      error: error => console.log(error),
      complete: () => console.log('Request completed successfully')
    })
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
