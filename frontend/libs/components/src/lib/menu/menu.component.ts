import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'educa-online-menu',
  standalone: false,
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})

export class MenuComponent {


  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {

  }

  ativo(menu: string): boolean {
    if(this.router.url.indexOf('usuario') >= 0 && menu === 'usuario')
      return true;
    else if(this.router.url.indexOf('curso') >= 0 && menu === 'curso')
      return true;

    return false;
  }
}
