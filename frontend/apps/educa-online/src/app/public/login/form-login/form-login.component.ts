import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AlertComponent, AlertOptions } from '@educa-online/components';
import { AuthService } from '@educa-online/services';
import { take } from 'rxjs';

@Component({
  selector: 'app-form-login',
  templateUrl: 'form-login.component.html',
  styleUrls: ['form-login.component.scss'],
  standalone: false
})

export class FormLoginComponent implements OnInit {

  private _snackBar = inject(MatSnackBar);

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.form = fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]]
    })
  }

  ngOnInit() { }


  login():void {
    const { value, valid } = this.form;

    if(valid) {
      this.authService.login(value)
      .pipe(take(1))
      .subscribe({
        next: (token) => {
          if(token) {
            console.log(token);
            this.authService.setToken(JSON.stringify(token));
            this.authService.setUrl('inicio');
            this.router.navigate(['/inicio']);
          }
        },
        error: (err) => {
          this._snackBar.openFromComponent(AlertComponent, {
            duration: 5000,
            data: {
              title: 'Erro!',
              subtitle: err.error,
              status: 'erro'
            } as AlertOptions
          });
        }
      });
    }
  }

  novaConta(): void {
    this.router.navigate(['/nova-conta']);
  }
}
