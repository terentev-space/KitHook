# KitHook

[![Latest Version](https://img.shields.io/github/release/terentev-space/KitHook.svg?style=flat-square)](https://github.com/terentev-space/KitHook/releases)
[![Software License](https://img.shields.io/badge/license-Apache_2.0-brightgreen.svg?style=flat-square)](LICENSE)

#### 🚧 Attention: the project is currently under development! 🚧

## Install


## Usage


## Examples
#### Queue

methods: `get`, `put`, `post`, `delete`, `head`, `options`, `patch`, `trace` 

types: `http`

```json
{
  "method": "post",
  "uri": "https://example.com?key1=value1",
  "content": null,
  "properties": {
    "property1": "value1"
  },
  "headers": {
    "header1": "value1"
  },
  "id": "yourId-123",
  "type": "http"
}
```

#### Contents

types: `none`, `form`, `text`, `json`, `byte`, `else`

Content (Form)
```json
{
  "data": {
    "key1": "value1",
    "key2": "value2"
  },
  "type": "form"
}
```

Content (Text)
```json
{
  "data": "value1",
  "type": "text"
}
```

Content (Json)
```json
{
  "data": {
    "Attr1": 1,
    "Attr2": "value2",
    "Attr3": [
      "value3_1",
      "value3_2"
    ]
  },
  "type": "json"
}
```

Content (Byte)
```json
{
  "data": "AAECAwQF",
  "type": "byte"
}
```

Content (Else)
```json
{
  "data": "<html>value</html>",
  "format": "text/html",
  "type": "else"
}
```

## Credits

- [Ivan Terentev](https://github.com/terentev-space)
- [All Contributors](https://github.com/terentev-space/KitHook/contributors)

## License

The Apache 2.0 License (Apache-2.0). Please see [License File](LICENSE) for more information.
