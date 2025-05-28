# GuidoAloise

Sito web dedicato al pittore Guido Aloise composto da un front-end Blazor WebAssembly e da una Web API ASP.NET Core.

Per avviare l'API è disponibile un `Dockerfile` nella cartella radice. Dopo aver installato Docker è sufficiente eseguire:

```bash
docker build -t guidoaloise-api -f Dockerfile .
docker run -p 5000:8080 guidoaloise-api
```

Il front-end può essere servito da qualunque server statico distribuendo i file contenuti nella cartella `GuidoAloise/wwwroot`.
