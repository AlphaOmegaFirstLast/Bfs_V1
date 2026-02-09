/**
 * Interface defining the structure for cookie options.
 * Using an interface improves type safety and readability.
 */
interface CookieOptions {
  /** Specifies the SameSite attribute for the cookie. */
  sameSite: 'Lax' | 'Strict' | 'None';
  /** Indicates whether the cookie should be sent only over HTTPS. */
  secure: boolean;
  /** The path for which the cookie is valid. */
  path: string;
}

/**
 * Sets a cookie with the provided name, value, and options.
 *
 * @param name The name of the cookie.
 * @param value The value to store in the cookie.
 * @param options An object containing the cookie attributes like sameSite, secure, and path.
 */
function setCookie(name: string, value: string, options: CookieOptions): void {
  // Conditionally adds the 'Secure' attribute if the secure option is true.
  // The attribute is just a flag, so its presence is what matters.
  const secureAttribute = options.secure ? ' Secure;' : '';
  
  // Constructs the complete cookie string using a template literal.
  const cookieString = `${name}=${encodeURIComponent(value)}; SameSite=${options.sameSite}; Path=${options.path};${secureAttribute}`;
  
  // Assigns the new cookie string to the document.
  document.cookie = cookieString;
}

/**
 * Retrieves the value of a cookie by its name.
 *
 * @param name The name of the cookie to retrieve.
 * @returns The decoded value of the cookie, or null if the cookie is not found.
 */
function getCookie(name: string): string | null {
  // Prepends the cookie name with '=' to avoid matching partial names.
  const nameEQ = name + '=';
  // Splits the document.cookie string into an array of individual cookies.
  const ca = document.cookie.split(';');
  
  // Loops through the array of cookies to find a match.
  for (let i = 0; i < ca.length; i++) {
    let c = ca[i];
    // Trims any leading whitespace.
    while (c.charAt(0) === ' ') {
      c = c.substring(1, c.length);
    }
    // Checks if the cookie starts with the desired name.
    if (c.indexOf(nameEQ) === 0) {
      // If found, decodes the URI component and returns the value.
      return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
  }
  // Returns null if the cookie is not found.
  return null;
}
export { setCookie, getCookie };