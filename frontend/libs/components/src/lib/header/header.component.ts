import { AuthService } from '@educa-online/services';
import { HeaderService } from '@educa-online/services';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'educa-online-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  titulo?: string = "Dashboard";
  destroy$ = new Subject<void>;

  constructor(
    private router: Router,
    private headerService: HeaderService,
    // public authService: AuthService
  ) {
    this.headerService.obterNovoTitulo()
      .pipe(takeUntil(this.destroy$))
      .subscribe(titulo => this.titulo = titulo);

    // this.authService.decodeToken();
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroy$.next();
  }

  logout(): void {
    // this.authService.logout();
  }
}
