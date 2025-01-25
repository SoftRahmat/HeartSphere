import { Component, OnInit, Signal } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    BsDropdownModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  loggedIn: boolean = false;

  constructor(private fb: FormBuilder, public _accountService: AccountService) {}
  ngOnInit() {
    this.initForm();
  }

  initForm(): void {
    this.loginForm = this.fb.group({
      userName: ['', [Validators.required, Validators.maxLength(50)]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (!this.loginForm.valid) {
      return;
    }

    const { userName, password } = this.loginForm.getRawValue();
    const payload = {
      username: userName,
      password: password,
    }

    this._accountService.login(payload).subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.error(err.error)
      }
    });
  }

  logout() {
    this._accountService.logout();
  }
}
