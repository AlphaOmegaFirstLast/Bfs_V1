// Set the desserialized fields to camelCase to match the interface fields
function camelize(obj: any): any {
  if (Array.isArray(obj)) {
    return obj.map(v => camelize(v));
  } else if (obj !== null && obj.constructor === Object) {
    return Object.keys(obj).reduce((result, key) => {
      const camelKey = key.replace(/_([a-z])/g, (_, c) => c.toUpperCase());
      result[camelKey] = camelize(obj[key]);
      return result;
    }, {} as any);
  }
  return obj;
}

export { camelize };