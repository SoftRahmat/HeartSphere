import { Component, Inject, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatListModule} from '@angular/material/list';
import { NavbarComponent } from "./navbar/navbar.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatListModule, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private _accountService = Inject(AccountService);
  title: string = 'Heart Sphere';
  users: any = {};

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(): void {
    const userString = localStorage.getItem('user');

    if (!userString) return;
    const user = JSON.parse(userString);
    this._accountService.currentUser.set(user);
  }
}
