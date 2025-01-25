import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Login } from '../_models/login-model';
import { User } from '../_models/user.model';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = 'https://localhost:5001/api';
  currentUser = signal<User | null>(null)

  constructor(private http: HttpClient) { }

  login(data: Login) {
    return this.http.post<User>(`${this.baseUrl}/account/login`, data).pipe(
      map( user=> {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    );
  }

  register(data: Login) {
    return this.http.post<User>(`${this.baseUrl}/account/register`, data).pipe(
      map( user=> {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
