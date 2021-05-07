import { UserManager, User, WebStorageStateStore, Profile } from 'oidc-client';
import { ApplicationName, ApplicationPaths } from './ApiAuthorizationConstants';


export class AuthorizeService {
    public userManager: UserManager;

    private _callbacks:any[] = [];
    private _nextSubscriptionId: number = 0;
    private _user: User = null;
    private _isAuthenticated: boolean;

    /** Vrací, zda-li je uživatel přihlášen. */
    public async isAuthenticated(): Promise<boolean> {
        const user = await this.getUserProfile();
        return !!user;
    }

    public getUserUnsafe(): User {
        return this._user;
    }

    public async getUser(): Promise<User> {
        await this.ensureUserManagerInitialized();
        let user = await this.userManager.getUser();

        if (!user || user.expired) {
            await this.userManager.createSigninRequest();
            this._user = null;
            user = null;
        }

        return user;
    }

    /** Vrací profil aktuálně přihlášeného uživatele. */
    public async getUserProfile(): Promise<Profile> {
        if (this._user && this._user.profile) {
            return this._user.profile;
        }

        const user = await this.getUser();
        return user && user.profile;
    }

    /** Vrací aktuální access token. */
    public async getAccessToken(): Promise<string> {
        const user = await this.getUser();
        return user && user.access_token;
    }

    public getUnsafeAccessToken(): string {
        return !!this._user ? this._user.access_token : null;
    }

    /** Pokusí se přihlásit uživatele. */
    public async signIn(state): Promise<AuthenticationResult> {
        await this.ensureUserManagerInitialized();

        try {
            const silentUser = await this.userManager.signinSilent(this.createArguments());
            this.updateState(silentUser);
            return this.success(state);
        } catch (err) {
            try {
                await this.userManager.signinRedirect(this.createArguments(state));
                return this.redirect();
            } catch (redirectError) {
                return this.error(redirectError);
            }
        }
    }

    /** Pokusí se přihlásit uživatele. */
    public async completeSignIn(url: string): Promise<AuthenticationResult> {
        try {
            await this.ensureUserManagerInitialized();
            const user = await this.userManager.signinCallback(url);
            this.updateState(user);
            return this.success(user && user.state);
        } catch (error) {
            return this.error('There was an error signing in.');
        }
    }

    /** Pokusí se odhlásit uživatele. */
    public async signOut(state: any): Promise<AuthenticationResult> {
        await this.ensureUserManagerInitialized();

        try {
            await this.userManager.signoutRedirect(this.createArguments(state));
            return this.redirect();
        } catch (redirectSignOutError) {
            return this.error(redirectSignOutError);
        }
    }

    /** Pokusí se odhlásit uživatele. */
    public async completeSignOut(url: string): Promise<AuthenticationResult> {
        await this.ensureUserManagerInitialized();
        try {
            const response = await this.userManager.signoutCallback(url);
            this.updateState(null);
            return this.success(response && response.state);
        } catch (error) {
            return this.error(error);
        }
    }

    /** Aktualizuje stav tohoto objektu podle stavu uživatele. */
    private updateState(user: User): void {
        this._user = user;
        this._isAuthenticated = !!this._user;
        this.notifySubscribers();
    }

    /** Přidá callback do kolekce "odebíraných" metod. */
    public subscribe(callback): number {
        this._callbacks.push({ callback, subscription: this._nextSubscriptionId++ });
        callback();
        return this._nextSubscriptionId - 1;
    }

    /** Odebere callback z kolekce "odebíraných" metod. */
    public unsubscribe(subscriptionId: number): void {
        const subscriptionIndex = this._callbacks
            .map((element, index) => element.subscription === subscriptionId ? { found: true, index } : { found: false })
            .filter(element => element.found === true);

        if (subscriptionIndex.length !== 1) {
            throw new Error(`Found an invalid number of subscriptions ${subscriptionIndex.length}`);
        }

        this._callbacks.splice(subscriptionIndex[0].index, 1);
    }

    /** Zavolá všechny "odebírané" metody. */
    private notifySubscribers() {
        for (let i = 0; i < this._callbacks.length; i++) {
            const callback = this._callbacks[i].callback;
            callback();
        }
    }

    private createArguments(state?: any) {
        return { useReplaceToNavigate: true, data: state };
    }

    /** Vrací chybu po vykonání operace. */
    private error(message: string): AuthenticationResult {
        return { status: AuthenticationResultStatus.Fail, message };
    }

    /** Vrací úspěch po vykonání operace. */
    private success(state?: any): AuthenticationResult {
        return { status: AuthenticationResultStatus.Success, state };
    }

    /** Vrací přesměrování po vykonání operace. */
    private redirect(): AuthenticationResult {
        return { status: AuthenticationResultStatus.Redirect };
    }

    private async ensureUserManagerInitialized() {
        if (this.userManager !== undefined) {
            return;
        }
        let response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);

        if (!response.ok) {
            throw new Error(`Could not load settings for '${ApplicationName}'`);
        }

        let settings = await response.json();
        settings.automaticSilentRenew = true;
        settings.includeIdTokenInSilentRenew = true;
        settings.userStore = new WebStorageStateStore({
            prefix: ApplicationName
        });

        this.userManager = new UserManager(settings);

        this.userManager.events.addUserSignedOut(async () => {
            await this.userManager.removeUser();
            this.updateState(undefined);
        });
    }

    /** Metoda pro získání aktuální instance tohoto objektu. */
    static get instance() { return authService }
}

// Vytvořím instanci této třídy pro přístup zvenčí.
const authService = new AuthorizeService();
export default authService;

/** Výsledek operace této třídy. */
export interface AuthenticationResult {
    status: AuthenticationResultStatus,
    message?: string,
    state?: any
}

/** Výčet typů výsledků operací této třídy. */
export enum AuthenticationResultStatus {
    Redirect,
    Success,
    Fail
}