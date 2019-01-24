import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  constructor(private authServise: AuthService, private router: Router) { }

  ngOnInit() {
  }
  logout() {
    this.authServise.logout();
    this.router.navigate(['/login']);
  }
}
