/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2022/11/23 20:28:52                          */
/*==============================================================*/


drop procedure if exists p_demo_get_user_course;

drop view if exists v_demo_user_course;

drop view if exists v_admin_user_role_priv;

drop view if exists v_admin_user_owner_priv;

drop table if exists admin_dicts;

drop table if exists admin_group;

drop table if exists admin_listedit;

drop table if exists admin_listedit_edititem;

drop table if exists admin_listedit_gridcolumn;

drop table if exists admin_listedit_queryitem;

drop table if exists admin_menu;

drop table if exists admin_msg;

drop table if exists admin_oper_log;

drop table if exists admin_req_log;

drop table if exists admin_role;

drop table if exists admin_role_menu;

drop table if exists admin_site;

drop table if exists admin_user;

drop table if exists admin_user_priv;

drop table if exists demo_class;

drop table if exists demo_course;

drop table if exists demo_user;

drop table if exists demo_user_course;

/*==============================================================*/
/* Table: admin_dicts                                           */
/*==============================================================*/
create table admin_dicts
(
   DictID               varchar(40) not null  comment '����GUID',
   Category             varchar(50)  comment '���',
   Name                 varchar(200)  comment '����',
   Value                text  comment 'ֵ',
   Type                 int not null default 0  comment '����0-ϵͳ1-�û�����',
   primary key (DictID)
);

alter table admin_dicts comment '��̨�ֵ�';

/*==============================================================*/
/* Table: admin_group                                           */
/*==============================================================*/
create table admin_group
(
   GroupID              int not null  comment '����',
   GroupName            varchar(100)  comment '����',
   `Desc`               text  comment '����',
   ParentID             int  comment '������',
   primary key (GroupID)
);

alter table admin_group comment '���Ż���';

/*==============================================================*/
/* Table: admin_listedit                                        */
/*==============================================================*/
create table admin_listedit
(
   GenID                bigint not null auto_increment  comment '����',
   ConnectionStringName varchar(50)  comment '��ѯ�����ַ�����',
   QuerySQLSource       text  comment 'ԭʼSQL',
   QuerySQL             text  comment 'SQL������SelectStatement��json',
   QueryTitle           varchar(100)  comment '��ѯҳ��Title',
   PageSize             int not null default 20  comment 'Gridҳ��С',
   GridHeight           int not null default 600  comment 'Grid�߶�',
   TableName            varchar(100)  comment 'ɾ������',
   PrimaryKeys          varchar(2000)  comment '��������json���л�',
   HasDelete            bool not null default 0  comment '�Ƿ���ɾ��',
   DeleteSQL            varchar(1000)  comment 'ɾ��SQL',
   HasAdd               bool not null default 0  comment '�Ƿ������',
   AddSQL               text  comment '���SQL',
   HasEdit              bool not null default 0  comment '�Ƿ��б༭',
   HasView              bool not null default 0  comment '�Ƿ��в鿴',
   SelectSQL            text  comment '�༭��ȡ����SQL',
   EditSQL              text  comment '�༭SQL',
   EditTitle            varchar(100)  comment '��ӱ༭ҳ��Title',
   EditWidth            int not null default 0  comment '��ӱ༭ҳ����',
   HasDialog            bool not null default 0  comment '�Ƿ���Dialog���ܣ�ֻ֧�ֵ�һ������',
   DialogSQL            varchar(1000)  comment '�Ի���SQL',
   DialogFieldName      varchar(50)  comment '�Ի�������',
   DialogWidth          int not null default 0  comment '�Ի�����',
   DialogHeight         int not null default 0  comment '�Ի���߶�',
   Status               tinyint not null default 1  comment '״̬ 0-��ʼ 1-��Ч 2-��Ч',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼����',
   primary key (GenID)
);

alter table admin_listedit comment '�Զ���SQL��ѯ��ӱ༭ģ������';

/*==============================================================*/
/* Table: admin_listedit_edititem                               */
/*==============================================================*/
create table admin_listedit_edititem
(
   EditItemID           bigint not null auto_increment  comment '����',
   GenID                bigint not null  comment 'Gen����',
   ColumnName           varchar(50)  comment '����',
   IsPrimaryKey         bool not null default 0  comment '�Ƿ�����',
   ControlType          varchar(50)  comment '�ؼ�����',
   ControlID            varchar(50)  comment '�ؼ�ID',
   FieldLabel           varchar(50)  comment 'Label',
   RowIndex             int not null default 0  comment '������',
   ColumnIndex          int not null default 0  comment '������',
   WidthNum             int not null default 0  comment '���',
   EditParameterName    varchar(50)  comment '������',
   EditDbType           varchar(50)  comment '��������',
   EditDotNetType       varchar(100)  comment '����DotNet����',
   DefaultValueSet      varchar(50)  comment 'Ĭ��ֵ����',
   DefaultValueString   varchar(50)  comment 'Ĭ��ֵ�ַ���',
   JsonData             text  comment 'Jsonϵ�л�',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼����',
   primary key (EditItemID)
);

alter table admin_listedit_edititem comment '��ӱ༭�ؼ�����';

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on admin_listedit_edititem
(
   GenID
);

/*==============================================================*/
/* Table: admin_listedit_gridcolumn                             */
/*==============================================================*/
create table admin_listedit_gridcolumn
(
   GridColumnID         bigint not null auto_increment  comment '����',
   GenID                bigint not null  comment '����',
   OrderNum             int not null default 0  comment '����',
   ColumnType           varchar(50)  comment '������',
   IsPrimaryKey         bool not null default 0  comment '�Ƿ�����',
   FieldName            varchar(50)  comment '�����ֶ���',
   ColumnName           varchar(50)  comment '���ݱ�����',
   Text                 varchar(50)  comment '��ʾ����',
   Align                varchar(50)  comment '���뷽ʽ',
   Width                varchar(10)  comment '�п�',
   Flex                 int  comment '�п�flex',
   Locked               bool not null default 0  comment '�Ƿ�����',
   Visible              bool not null default 1  comment '�Ƿ�ɼ�',
   Format               varchar(50)  comment '��ʾ��ʽ',
   TrueText             varchar(50)  comment 'BooleanColumn true�ı�',
   FalseText            varchar(50)  comment 'BooleanColumn false�ı�',
   FilterType           varchar(50)  comment '��������',
   RenderFn             varchar(50)  comment '�����ʾ������',
   RenderFnContent      text  comment '�����ʾ��������',
   RenderHandler        varchar(1000)  comment '�����ʾhandler',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼����',
   primary key (GridColumnID)
);

alter table admin_listedit_gridcolumn comment '��ѯgrid�ж���';

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on admin_listedit_gridcolumn
(
   GenID,
   OrderNum
);

/*==============================================================*/
/* Table: admin_listedit_queryitem                              */
/*==============================================================*/
create table admin_listedit_queryitem
(
   QueryItemID          bigint not null auto_increment  comment '����',
   GenID                bigint not null  comment 'Gen����',
   QueryBlock           varchar(255)  comment '��ѯ����',
   QueryParameterName   varchar(50)  comment '��ѯ������',
   QueryDbType          varchar(50)  comment '����DbType����',
   QueryDotNetType      varchar(100)  comment '����DotNet����',
   ControlType          varchar(50)  comment '�ؼ�����',
   ControlID            varchar(50)  comment '�ؼ�ID',
   FieldLabel           varchar(50)  comment 'Label',
   RowIndex             int not null default 0  comment '������',
   ColumnIndex          int not null default 0  comment '������',
   JsonData             text  comment 'Jsonϵ�л�',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼����',
   primary key (QueryItemID)
);

alter table admin_listedit_queryitem comment '��ѯ�����ؼ�����';

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on admin_listedit_queryitem
(
   GenID
);

/*==============================================================*/
/* Table: admin_menu                                            */
/*==============================================================*/
create table admin_menu
(
   MenuID               varchar(40) not null  comment '�˵�����GUID',
   SiteID               varchar(50)  comment 'վ�����',
   ParentId             varchar(40) default '0'  comment '���˵����� 0-���˵�',
   Title                varchar(50)  comment '�˵���ʾ����',
   OrderNum             int not null default 0  comment '���򣬴�С����',
   Icon                 varchar(250)  comment 'ͼ��',
   LinkMode             int not null default 0  comment '��������0-Ŀ¼1-rul 2-siteid+url 3-GenID',
   Url                  varchar(250)  comment '�˵�URL������ʹ�����·��',
   GenID                varchar(50)  comment 'GenID����',
   UrlTarget            tinyint not null default 0  comment '����ģʽ 0-TAB�� 1-�´��ڴ�',
   PrivParams           varchar(250)  comment '���ܺ�����Ȩ�޲�������ʽ������-����| ����-����
             �����ڶ���ҳ����Ȩ��ʱ�����õ�Ȩ��ѡ���б�
             �磺ControlID-btnOk|ControlID-btnCancle',
   Pinyin               varchar(20)  comment 'ƴ��',
   `Desc`               varchar(500)  comment '����',
   Status               tinyint not null default 0  comment '״̬ 0-��Ч 1-��Ч',
   primary key (MenuID)
);

alter table admin_menu comment '����˵�';

/*==============================================================*/
/* Table: admin_msg                                             */
/*==============================================================*/
create table admin_msg
(
   MsgID                varchar(40) not null  comment '����GUID',
   UserID               varchar(40)  comment '�û�',
   Flag                 int not null  comment '��ʶ',
   `From`               varchar(50)  comment '��Դ',
   Title                varchar(100)  comment '����',
   Content              text  comment '����',
   `Label`              varchar(50)  comment '',
   Status               int not null  comment '״̬0-δ֪1-��Ч2-ɾ��',
   SendDate             datetime not null  comment '����ʱ��',
   primary key (MsgID)
);

alter table admin_msg comment '��Ϣ';

/*==============================================================*/
/* Table: admin_oper_log                                        */
/*==============================================================*/
create table admin_oper_log
(
   LogID                varchar(40) not null  comment '��־����GUID',
   OperType             varchar(150) default '0'  comment '���������ࣨ����ҵ������Լ����
             sql --���ݿ����
             ',
   Title                varchar(255)  comment '��������',
   Content              text  comment '����',
   Tag1                 varchar(100)  comment '���1',
   Tag2                 varchar(100)  comment '���2',
   Tag3                 varchar(100)  comment '���3',
   Tag4                 varchar(100)  comment '���3',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼ʱ��',
   primary key (LogID)
);

alter table admin_oper_log comment '��̨���������־';

/*==============================================================*/
/* Index: Index_1                                               */
/*==============================================================*/
create index Index_1 on admin_oper_log
(
   RecDate,
   OperType
);

/*==============================================================*/
/* Table: admin_req_log                                         */
/*==============================================================*/
create table admin_req_log
(
   LogID                varchar(40) not null  comment '����GUID',
   UserID               varchar(40)  comment '�����û�ID',
   Type                 int not null  comment '����0-��¼1-����',
   Result               varchar(50)  comment '���',
   RequestUrl           varchar(255)  comment '�����ַ',
   IP                   varchar(50)  comment 'IP��ַ',
   OS                   varchar(50)  comment 'ϵͳ',
   Browser              varchar(50)  comment '�����',
   Location             varchar(100)  comment '��ַ',
   UserAgent            text  comment '����',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼����',
   primary key (LogID)
);

alter table admin_req_log comment '��¼������־';

/*==============================================================*/
/* Table: admin_role                                            */
/*==============================================================*/
create table admin_role
(
   RoleID               int not null  comment '��ɫ��ʶ',
   RoleName             varchar(50)  comment '��ɫ����',
   `Desc`               varchar(255)  comment '����',
   Status               tinyint not null default 0  comment '״̬ 0-��Ч 1-��Ч',
   primary key (RoleID)
);

alter table admin_role comment '��ɫ��';

/*==============================================================*/
/* Table: admin_role_menu                                       */
/*==============================================================*/
create table admin_role_menu
(
   RoleID               int not null  comment 'Ȩ�ޱ�ʶ',
   MenuID               varchar(40) not null  comment '�˵�����GUID',
   PrivParamsValue      text  comment '���ܺ�����Ȩ�޲�����
             ����-����-�Ƿ���Ȩ��| ����-����-�Ƿ���Ȩ��
             �磺ControlID-btnOk-true|ControlID-btnCancle-false
             
             ',
   primary key (RoleID, MenuID)
);

alter table admin_role_menu comment '�û��˵�Ȩ�ޣ��Ե����˵���Ȩ�������Ŀ¼';

/*==============================================================*/
/* Table: admin_site                                            */
/*==============================================================*/
create table admin_site
(
   SiteID               varchar(50) not null  comment 'վ�����',
   SiteName             varchar(50)  comment 'վ������',
   BaseUrl              varchar(255)  comment '����·��',
   `Desc`               text  comment '����',
   Status               tinyint not null default 0  comment '״̬ 0-��Ч 1-��Ч',
   primary key (SiteID)
);

alter table admin_site comment '��̨����վ��';

/*==============================================================*/
/* Table: admin_user                                            */
/*==============================================================*/
create table admin_user
(
   UserID               varchar(40) not null  comment '�û�ID GUID',
   UserName             varchar(50)  comment '��¼�û���',
   Password             varchar(255)  comment '��¼����',
   PasswordSalt         varchar(40)  comment '�����ϣSalt',
   Mobile               varchar(20)  comment '�ֻ���',
   DisplayName          varchar(20)  comment '��ʾ�û���',
   `Desc`               text  comment '����',
   GroupID              int  comment '�������',
   IsAdmin              bool not null default 0  comment '�Ƿ����Ա',
   RegisterDate         datetime not null  comment 'ע��ʱ��',
   ApprovedDate         datetime  comment '����ʱ��',
   ApprovedBy           varchar(50)  comment '������',
   RejectDate           datetime  comment '�ܾ�ʱ��',
   RejectBy             varchar(50)  comment '�ܾ���',
   Status               tinyint not null default 0  comment '״̬ 0-��Ч 1-��Ч',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '��¼����',
   Icon                 varchar(255)  comment 'ͷ��',
   primary key (UserID)
);

alter table admin_user comment '��̨�û���
where status=1 ʱ usernameΨһ';

/*==============================================================*/
/* Table: admin_user_priv                                       */
/*==============================================================*/
create table admin_user_priv
(
   UserID               varchar(40) not null  comment '�����û�ID',
   PrivType             int not null default 0  comment 'Ȩ������ 0-site 1-role 2-menu',
   PrivID               varchar(100) not null  comment '��ɫ�����˵�����',
   IsEnabled            bool not null default 1  comment '�Ƿ�����',
   PrivParamsValue      text  comment '���ܺ�����Ȩ�޲�������ʽ������-����| ����-����
             �����ڶ���ҳ����Ȩ��ʱ�����õ�Ȩ��ѡ���б�
             �磺ControlID-btnOk|ControlID-btnCancle',
   primary key (UserID, PrivType, PrivID)
);

/*==============================================================*/
/* Table: demo_class                                            */
/*==============================================================*/
create table demo_class
(
   ClassID              varchar(10) not null  comment '������',
   Name                 varchar(10)  comment '���',
   Sort1                int not null  comment '',
   Sort2                int not null  comment '',
   primary key (ClassID)
);

alter table demo_class comment '���
�����кܶ�˵��';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create unique index index_1 on demo_class
(
   Sort1,
   Sort2
);

/*==============================================================*/
/* Table: demo_course                                           */
/*==============================================================*/
create table demo_course
(
   Year                 year(4) not null  comment 'ѧ��',
   CourseID             char(36) not null  comment '�γ̱��루GUID��',
   Name                 varchar(10)  comment '����',
   OrderNum             int not null  comment '',
   primary key (Year, CourseID)
);

alter table demo_course comment '�γ�';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create unique index index_1 on demo_course
(
   OrderNum
);

/*==============================================================*/
/* Table: demo_user                                             */
/*==============================================================*/
create table demo_user
(
   UserID               bigint not null auto_increment  comment '�û����루�����ֶΣ�',
   ClassID              varchar(10) not null  comment '������',
   FBit                 bit not null default 1  comment '�ֶ�1
                     ����1
                     ����2',
   FBit_Max             bit(64)  comment '',
   FTinyInt             tinyint not null default 127  comment '',
   FTinyInt_Unsigned    tinyint unsigned default 255  comment '',
   FBool                bool  comment '',
   F_Boolean            boolean not null default 1  comment '',
   FBool_TinyInt        tinyint(1) default 0  comment '',
   FSmallInt            smallint not null default -32768  comment '',
   FSmallInt_Unsigned   smallint unsigned default 65535  comment '',
   FMediumInt           mediumint not null default -8388608  comment '',
   FMediumInt_Unsigned  mediumint unsigned  comment '',
   FInt                 int not null default -2147483648  comment '',
   FInt_Unsigned        int unsigned default 4294967295  comment '',
   F_Integer            Integer  comment '',
   FBigInt              bigint not null default -9223372036854775808  comment '',
   FBigInt_Unsigned     bigint unsigned  comment '',
   FFloat               float not null default 12.345678  comment '',
   FFloat_Max           float(7,4) unsigned  comment '',
   FDouble              double not null default 123456789.1234567  comment '',
   FDouble_Max          double(15,4) unsigned  comment '',
   F_Real               real  comment '',
   F_Double_Precision   double precision unsigned  comment '',
   FYear                year(4)  comment '',
   FDate                date  comment '',
   FTime                time  comment '',
   FTimestamp           timestamp default CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP  comment '',
   FDateTime            datetime  comment '',
   FChar                char(4)  comment '',
   FVarChar             varchar(255)  comment '',
   FBinary              binary(2)  comment '',
   FVarBinary           varbinary(2)  comment '',
   FTinyText            tinytext  comment '',
   FText                text  comment '',
   FMediumText          mediumtext  comment '',
   FLongText            longtext  comment '',
   FTinyBlob            tinyblob  comment '',
   FBlob                blob  comment '',
   FMediumBlob          mediumblob  comment '',
   FLongBlob            longblob  comment '',
   FEnum                enum('m','f')  comment '',
   FSet                 set('one','two')  comment '',
   FDecimal             decimal not null default 123.45  comment '',
   FDecimal_Max         decimal(65,30) unsigned  comment '',
   F_Numeric            numeric  comment '',
   F_Dec                dec  comment '',
   F_Fixed              fixed  comment '',
   primary key (UserID)
);

alter table demo_user comment '�û���';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create index index_1 on demo_user
(
   FInt
);

/*==============================================================*/
/* Index: index_3                                               */
/*==============================================================*/
create index index_3 on demo_user
(
   FYear,
   FDate
);

/*==============================================================*/
/* Table: demo_user_course                                      */
/*==============================================================*/
create table demo_user_course
(
   UserID               bigint not null  comment '�û����루�����ֶΣ�',
   Year                 year(4) not null  comment 'ѧ��',
   CourseID             char(36) not null  comment '�γ̱��루GUID��',
   Note                 text  comment '˵��',
   primary key (UserID, CourseID, Year)
);

alter table demo_user_course comment '�û������';

/*==============================================================*/
/* View: v_admin_user_owner_priv                                */
/*==============================================================*/
create VIEW  v_admin_user_owner_priv
 as
select b2.*, -1 as RoleID, b1.PrivParamsValue,b1.IsEnabled, b1.UserID from admin_user_priv b1
	join admin_menu b2 on b1.PrivID=b2.MenuID and b2.`status`=1
where b1.PrivType=2;

/*==============================================================*/
/* View: v_admin_user_role_priv                                 */
/*==============================================================*/
create VIEW  v_admin_user_role_priv
 as
select a2.*, a1.IsEnabled,a1.UserID from admin_user_priv a1 
	join 
		(
		select t2.*,t1.RoleID,t1.PrivParamsValue 
		from admin_role_menu t1
			join admin_menu t2 on t1.MenuID = t2.MenuId and t2.`status`=1
			join admin_role t3 on t1.RoleID = t3.RoleID and t3.`status`=1
		) a2 on a1.PrivID=a2.RoleID 
where a1.PrivType=1;

/*==============================================================*/
/* View: v_demo_user_course                                     */
/*==============================================================*/
create VIEW  v_demo_user_course
 as
select t2.UserID, t2.ClassID, t3.CourseID, t3.Name, t1.Note, '������' as TestColumn
from demo_user_course as t1
	left join demo_user as t2 on t1.UserID = t2.UserID
	left join demo_course as t3 on t1.CourseID = t3.CourseID;


create procedure p_demo_get_user_course(in pUserID bigint,out pPageCount int)
comment '�洢��������'
begin
	select count(0) into pPageCount from demo_user_course where UserID = pUserID;
	select * from demo_user_course where UserID = pUserID;
	select 'abc';
	-- return 123
end;

