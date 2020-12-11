# Hubee Notification Service

![N|Solid](https://media-exp1.licdn.com/dms/image/C4E0BAQHOp41isf2byw/company-logo_200_200/0?e=1611792000&v=beta&t=R627Tkw1cwQgb-LjNTJh_4auJWQsQieuU4wHoyLfIDA)

Hubee Notification Service é um serviço responsável por todo o gerenciamento da entrega de mensagens. A principal ideia dessa solução é abstrair todo o processo de envio e criação de notificações, para os devidos destinatários da mensagem.

## Tipos de notificações

As notificações de mensagens podem ocorrer por diversos tipos, segue abaixo os tipos de notificações implementadas por esse serviço:

| Tipo | Observação |
|:----|:----------|
| E-mail | as notificações serão enviadas por e-mail para um ou mais destinatários |

## Template da mensagem

Para construção das mensagens criamos uma estrutura de template que fica armazenada no banco de dados, com o objetivo de fazer a manutenção dos templates.

Estrutura:

```sql
CREATE TABLE "Template"
(
    "Id" uuid NOT NULL,
    "NotificationType" integer NOT NULL,
    "TemplateType" integer NOT NULL,
    "TemplateVersion" integer NOT NULL,
    "Title" character varying(150) NOT NULL,
    "Content" text NOT NULL,
    "IsRendered" boolean NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    "DeletedAt" timestamp without time zone,
    CONSTRAINT "PK_Template" PRIMARY KEY ("Id")
)
```

| Coluna | Valor | Observação |
|:----|:----------|:----------|
| Id | Guid | identificador único do template |
| NotificationType | 1: E-mail | tipo do envio da mensagem <br> o tipo E-mail suporta a utilização da sintaxe [MJML](https://documentation.mjml.io/) para o conteúdo da mensagem |
| TemplateType | 1: Recuperação de senha  | tipo do conteúdo da mensagem |
| TemplateVersion | 1: V1 | versionamento do template |
| Title | string | título da mensagem podendo ser personalizado com informações **chave-valor** |
| Content | string | conteúdo da mensagem podendo ser personalizado com informações **chave-valor** |
| IsRendered | boolean | caso o valor for **true** o conteúdo da mensagem será renderização de acordo com a sintaxe utilizada |
| CreatedAt | timestamp | data da criação do template |
| DeletedAt | timestamp | data da inativação do template |

## Contrato da notificação

Para criar uma notificação, deve-se, seguir o contrato abaixo:

```json
{
  "notificationType": 1,
  "templateType": 1,
  "templateVersion": 1,
  "receiver": ["luiz.gmail.com"],
  "templateMapper": {
      "title":[{"Key":"@nome","Value":"luiz"}],
      "message":[{"Key":"@nome","Value":"Luiz"},{"Key":"@idade","Value":"25"}]
    }
}
```

**OBS:** os dados do json são apenas exemplos de utilização

| Propriedade | Observação |
|:----|:----------|
| notificationType | informar o tipo da notificação |
| templateType | informar o tipo do template |
| templateVersion | informar a versão do tamplate |
| receiver | informar um ou mais destinatários da mensagem |
| templateMapper | caso o tamplete seja personalizado é necessário o mapeamento, e o mesmo é basedado em **chave-valor**. Sendo que a chave deve corresponder com a mesma utilizada no template |
| templateMapper.title | informar o mapeamento para o título da mensagem |
| templateMapper.message | informar o mapeamento para o conteúdo da mensagem |

E para o envio das notificações o serviço utiliza tanto a comunicação em eventos (pub/sub) ou http (REST).
