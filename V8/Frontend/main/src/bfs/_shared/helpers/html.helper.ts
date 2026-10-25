import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

// const greenArrow = "<span style='font-size:large; font-weight:bold; color: rgba(10, 150, 82, 0.72);'> &uarr; </span>";
// const greenDataStart = " <span style='font-size:16px; font-weight:bold; color: rgba(10, 150, 82, 0.72);'>";
// const greenDataEnd = "</span>";

// const redArrow = "<span style='font-size:large; font-weight:bold; color: red;'> &darr; </span>";
// const redDataStart = " <span style='font-size:16px; font-weight:bold; color: red;'>";
// const redDataEnd = "</span>";

// const blueArrow = "<span style='font-size:large; font-weight:bold; color: blue;'> &darr; </span>";
// const blueDataStart = " <span style='font-size:16px; font-weight:bold; color: blue;'>";
// const blueDataEnd = "</span>";

export function safeHtmlDecode(input: string | null | undefined): string | null | undefined {
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


export function setArrowData(sanitizer: DomSanitizer, color: string, arrow: string, data: any) {
  let colour = "";
  switch (color.toLowerCase()) {
    case "green":
      colour = "color:rgba(10, 150, 82, 0.72)";
      break;
    case "red":
      colour = "color:red";
      break;
    case "blue":
      colour = "color:blue";
      break;
  }

  const arrowUp = "&uarr;";
  const arrowDown = "&darr;";
  var arrowHtml = ``;
  switch (arrow.toLowerCase()) {
    case "up":
      arrowHtml = `<span style='font-size:large; font-weight:bold; ${colour};'> ${arrowUp} </span>`;
      break;
    case "down":
      arrowHtml = `<span style='font-size:large; font-weight:bold; ${colour};'> ${arrowDown} </span>`;
      break;
  }

  const dataStart = `<span style='font-size:14px; font-weight:bold; ${colour};'>`;
  const dataEnd = "</span>";
  const roundedData = data? ( Number(data)? (Math.round(data.trim() * 100) /100)  : data ) : "";

  var rawHtml = `${arrowHtml}  ${dataStart}  ${roundedData?.toString()}  ${dataEnd}`;

  return sanitizer ? sanitizer.bypassSecurityTrustHtml(rawHtml) : data?.toString();
}

