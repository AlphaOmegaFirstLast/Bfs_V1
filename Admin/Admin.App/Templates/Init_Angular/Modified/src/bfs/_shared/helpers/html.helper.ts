function safeHtmlDecode(input: string | null | undefined): string | null | undefined {
  if (!input) return input;

  // Check if string actually contains HTML entities
  // (&lt; &gt; &amp; &quot; &#123; etc.)
  const hasEntities = /&[#A-Za-z0-9]+;/.test(input);
  if (!hasEntities) return input; // nothing to decode

  // Decode using DOMParser (safe and efficient)
  const parser = new DOMParser();
  const doc = parser.parseFromString(input, "text/html");
  const decoded = doc.documentElement.textContent || "";

  return decoded;
}

export { safeHtmlDecode };