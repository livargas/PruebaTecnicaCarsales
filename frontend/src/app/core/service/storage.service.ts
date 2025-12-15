
import { isPlatformBrowser } from "@angular/common";
import { Inject, Injectable, PLATFORM_ID } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private isBrowser: boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: object) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  setItem<T>(key: string, value: T): void {
    try {
      const jsonValue = JSON.stringify(value);
      if (this.isBrowser) {
        localStorage.setItem(key, jsonValue);
      }
    } catch (error) {
      console.error(`Error guardando ${key} en local storage`, error);
    }
  }
  getItem<T>(key: string): T | null {
    try {
      if (this.isBrowser) {
        const value = localStorage.getItem(key);
        return value ? JSON.parse(value) : null;
      }
      return null;
    } catch (error) {
      console.error(`Error leyendo ${key} de local storage`, error);
      return null;
    }
  }

  removeItem(key: string): void {
    if (this.isBrowser) {
      localStorage.removeItem(key);
    }
  }

  clear(): void {
    if (this.isBrowser) {
      localStorage.clear();
    }
  }

}