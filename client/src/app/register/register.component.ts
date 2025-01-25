import { Component, Input, OnInit, output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit {
  cancelRegister = output<boolean>();
  signupForm: FormGroup = new FormGroup({});
  error: string = '';

  constructor(private fb: FormBuilder, private _accountService: AccountService) {}

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.signupForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (!this.signupForm.valid) {
      return;
    }

    const { username, password } = this.signupForm.getRawValue();
    const payload = {
      username: username,
      password: password,
    }

    this._accountService.register(payload).subscribe({
      next: res => {
        console.log(res);
        this.cancel();
      },
      error: err => {
        this.error = err.error;
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
