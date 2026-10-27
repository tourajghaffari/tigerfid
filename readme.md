![TigeRFID](_assets/tigerfid.jpg)
# TigeRFID

## Introduction

This repository has been created [in honor of our father](https://www.rfidjournal.com/articles/view?15751/), Touraj Ghaffari. He founded [ActiveWave, Inc.](http://www.activewaveinc.com/) in 2001. It was his life work and something he loved and enjoyed thoroughly. He also loved spreading knowledge. In honor of that principle, we, his three sons, have open-sourced everything associated with his active RFID system under the [Apache 2.0 license](license). We hope this release helps you acheive your dreams.

## What is this?

ActiveWave designed and built active RFID (Radio Frequency IDentification) products that work together to provide a new approach to tracking and security. The system includes the following features:

* Extended Read Range. A single RFID reader can cover up to 30,000 square feet of floor space.

* Multi-Tag Capability. Our tags employ anti-collision logic to assure each tag's information is read, even when multiple tags are attempting to transmit concurrently.

* Compact Low-Cost Design. Our exclusive design brings you the advantages of RFID technology, combined with long battery life and low cost.

* Simplified Installation. Our wireless device attachment capability simplifies the installation and interconnection of ActiveWave system components.

The [presentations](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/presentations) and [website](http://www.activewaveinc.com/) provide a great deal more detail - especially the [product datasheets](http://www.activewaveinc.com/products_datasheets.php).

## Where this is going

We are rebuilding the system from the ground up, so that a complete beginner can build it with free tools on Windows, macOS or Linux. The first build is one Reader, one Field Generator and three tags (a sealTag, a compactTag and a cardTag) across two zones.

A design study comes first. It decides the new system against three scenarios:

* a conference, where the software shows each attendee on a floorplan;
* a nursery, where each baby's tag carries a temperature sensor, so staff can find each baby and read the sensor;
* a warehouse that counts 100,000 items in minutes.

The work is planned in the open in [docs/project-plan.md](docs/project-plan.md).

### The original

Everything released in 2017 and 2018 is kept, byte for byte, at the tag [`activewave-2018`](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018), with its [release notes](https://github.com/tourajghaffari/tigerfid/releases/tag/activewave-2018) and the [2018 README](https://github.com/tourajghaffari/tigerfid/blob/activewave-2018/readme.md). The first release, [`1.0.0`](https://github.com/tourajghaffari/tigerfid/releases/tag/1.0.0) (February 2017), is the system as he left it. Folder by folder:

1. [API](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/api) - the APIs for C++ .NET, C# .NET, C# .NET CE and VB .NET, with their documentation.

2. [BOM files](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/bom-files) - the Bills Of Materials, with each part and its cost at the time.

3. [Documentation](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/documentation) - documents converted to Markdown.

4. [Patents](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/patents) - the patents that covered the technology.

5. [Presentations](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/presentations) - the marketing materials for different applications.

6. [Protel files](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/protel-files) - the schematics and board files, designed in Protel (now Altium).

7. [sealTag](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/sealtag) - the sealTag, the one device whose firmware source is in the tree.

8. [Software](https://github.com/tourajghaffari/tigerfid/tree/activewave-2018/software) - the applications that ran the system, among them the Programming Station and the Smart Tracker.

To work with the original on your own machine, clone without downloading all of its files up front (about 3 GB):

```
git clone --filter=blob:none https://github.com/tourajghaffari/tigerfid.git
```

That fetches the files on `main` only. `git checkout activewave-2018` then fetches the original's files as it checks them out.

### Getting involved

Issues and questions are welcome now: [open an issue](https://github.com/tourajghaffari/tigerfid/issues/new). Pull requests are welcome once the design study has set the direction. See [contributing](contributing.md).

## Fun Touraj Fact

Touraj's favorite animal was the tiger - in particular, the [Caspian tiger](https://en.wikipedia.org/wiki/Caspian_tiger). Just like this project has arisen from extinction, the Caspian tiger should hopefully [make a triumphant return](http://www.bbc.co.uk/newsbeat/article/38663101/scientists-want-to-bring-cousin-of-extinct-caspian-tiger-to-central-asia) as well!

## Thank You

* Ali Amjadi

* Mehdi Haidari

* [Fabiano Kovalski](https://www.linkedin.com/in/fabianogk)

* [Vincent Mamo](https://www.linkedin.com/in/vincent-spinella-mamo-5262524)

* Reza Mokhtarzadeh
