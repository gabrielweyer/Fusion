# Fusion

`Fusion` aggregates multiple `IIS` log files into a single file.

## Usage

```posh
dotnet .\Fusion.dll "<input-directory>" "<output-directory>"
```

- `<input-directory>`: directory where the `IIS` log files are located. `Fusion` will also read the subdirectories.
  - Log files are expected to use the `.log` extension.
- `<output-directory>`: directory where `Fusion` will write the resulting file using the following pattern for the name: `<guid>.log`.

Sample usage: `dotnet .\Fusion.dll "E:\tmp\logs\" "E:\tmp\"`

**Warning**: `Fusion` does not read the `Fields` from the header, currently it uses `date time s-sitename cs-method cs-uri-stem cs-uri-query s-port cs-username c-ip cs(User-Agent) cs(Cookie) cs(Referer) cs-host sc-status sc-substatus sc-win32-status sc-bytes cs-bytes time-taken`.
