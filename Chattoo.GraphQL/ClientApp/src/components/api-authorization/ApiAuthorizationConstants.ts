/**
 * Název aplikace
 */
export const ApplicationName = 'ClientApp';

/**
 * URL vedoucí na API endpoint (bez části určující protokol).
 */
export const API_URL_WITHOUT_PROCOTOL = 'localhost:5001';

/**
 * URL vedoucí na API endpoint.
 */
export const API_URL = 'https://' + API_URL_WITHOUT_PROCOTOL;

/**
 * Akce pro odhlášení.
 */
export const LogoutActions = {
  LogoutCallback: 'logout-callback',
  Logout: 'logout',
  LoggedOut: 'logged-out'
};

/**
 * Akce pro přihlášení.
 */
export const LoginActions = {
  Login: 'login',
  LoginCallback: 'login-callback',
  LoginFailed: 'login-failed',
  Profile: 'profile',
  Register: 'register'
};

const prefix = '/authentication';

/**
 * Routing.
 */
export const ApplicationPaths = {
  DefaultLoginRedirectPath: '/',
  ApiAuthorizationClientConfigurationUrl: `${API_URL}/_configuration/${ApplicationName}`,
  ApiAuthorizationPrefix: prefix,
  Login: `${prefix}/${LoginActions.Login}`,
  LoginFailed: `${prefix}/${LoginActions.LoginFailed}`,
  LoginCallback: `${prefix}/${LoginActions.LoginCallback}`,
  Register: `${prefix}/${LoginActions.Register}`,
  Profile: `${prefix}/${LoginActions.Profile}`,
  LogOut: `${prefix}/${LogoutActions.Logout}`,
  LoggedOut: `${prefix}/${LogoutActions.LoggedOut}`,
  LogOutCallback: `${prefix}/${LogoutActions.LogoutCallback}`,
  IdentityRegisterPath: `${API_URL}/Identity/Account/Register`,
  IdentityManagePath: `/${API_URL}Identity/Account/Manage`
}