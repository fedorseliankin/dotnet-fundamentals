using oop_principles.services;

ICachePolicy cachePolicy = new DocumentCachePolicy();
var app = new Application(new FileDocumentStorageService(cachePolicy));
app.Run();