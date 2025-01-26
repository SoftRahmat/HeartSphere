import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const _accountService = inject(AccountService);
  const _toaster = inject(ToastrService);

  if (_accountService.currentUser()) {
    return true;
  } else {
    _toaster.error('Access denied! Move along, traveler!')
    return false;
  }
};
