/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2020-08-05 17:00:54                          */
/*==============================================================*/


drop procedure if exists p_demo_get_user_course;

drop view if exists v_demo_user_course;

drop view if exists v_admin_user_priv;

drop view if exists v_admin_user_group;

drop view if exists v_admin_priv;

drop table if exists admin_gen_addedit;

drop table if exists admin_gen_query;

drop table if exists admin_group;

drop table if exists admin_group_privilege;

drop table if exists admin_menu;

drop table if exists admin_privilege;

drop table if exists admin_site;

drop table if exists admin_site_mapuser;

drop table if exists admin_user;

drop table if exists admin_user_group;

drop table if exists admin_user_privilege;

drop table if exists demo_user_course;

drop table if exists demo_class;

drop table if exists demo_course;

drop table if exists demo_user;

drop table if exists sys_china_area;

drop table if exists sys_log;

/*==============================================================*/
/* Table: admin_gen_addedit                                     */
/*==============================================================*/
create table admin_gen_addedit
(
   AddEditID            bigint not null auto_increment  comment '编码',
   ConnStrName          varchar(50)  comment '数据连接名称',
   ServerName           varchar(50) not null  comment '服务器',
   DatabaseName         varchar(50) not null  comment '数据库名称',
   TableName            varchar(50) not null  comment '数据库表名称',
   Title                varchar(100)  comment '页面标题',
   Width                varchar(20)  comment '窗体宽度',
   Height               varchar(20)  comment '窗体高度',
   GenPath              varchar(255)  comment '生成保存路径',
   Note                 varchar(255)  comment '描述',
   TableSchema          blob  comment '表的Schema数据',
   ColumnsData          blob  comment '序列化的列数据',
   Status               tinyint not null default 0  comment '状态 0-初始 1-有效 2-无效',
   RecDate              datetime not null default CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP  comment '记录日期',
   primary key (AddEditID)
);

alter table admin_gen_addedit comment '添加修改模板';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create unique index index_1 on admin_gen_addedit
(
   ServerName,
   DatabaseName,
   TableName
);

/*==============================================================*/
/* Table: admin_gen_query                                       */
/*==============================================================*/
create table admin_gen_query
(
   QueryID              bigint not null auto_increment  comment '查询模板主键',
   ConnStrName          varchar(50)  comment '数据连接名称',
   ServerName           varchar(50) not null  comment '服务器',
   DatabaseName         varchar(50) not null  comment '数据库名称',
   `Sql`                text  comment 'SQL语句',
   Title                varchar(100)  comment '页面标题',
   HasAdd               bool not null default 1  comment '是否有添加',
   HasEdit              bool not null default 1  comment '是否有编辑',
   HasView              bool not null default 0  comment '是否有查看',
   HasDelete            bool not null default 1  comment '是否有删除',
   AddEditID            bigint  comment '添加编辑生成模板编码',
   TableName            varchar(100)  comment '目标表名',
   PrimaryKey           blob  comment '目标表主键',
   PageSize             int not null  comment '页大小',
   QueryItems           blob  comment '查询控件配置数据',
   GridColumns          blob  comment 'GUID列配置数据',
   Note                 varchar(255)  comment '描述',
   GenPath              varchar(255)  comment '生成保存路径',
   Status               tinyint not null default 0  comment '状态 0-初始 1-有效 2-无效',
   RecDate              datetime not null default CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP  comment '记录日期',
   primary key (QueryID)
);

alter table admin_gen_query comment '查询模板';

/*==============================================================*/
/* Table: admin_group                                           */
/*==============================================================*/
create table admin_group
(
   GroupID              bigint not null auto_increment  comment '用户组ID',
   GroupName            varchar(20)  comment '组名称',
   Status               tinyint not null default 0  comment '状态 0-无效 1-有效',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '记录日期',
   primary key (GroupID)
);

alter table admin_group comment '用户组';

/*==============================================================*/
/* Table: admin_group_privilege                                 */
/*==============================================================*/
create table admin_group_privilege
(
   GroupID              bigint not null  comment '用户组ID',
   PrivilegeID          bigint not null  comment '权限ID',
   PrivParamsValue      text  comment '分配给组的页面内权限。json格式
             如：[{Type:ControlID,Params:btnOk,Value:false},{Type:ControlID,Params:btnOk,Value:true}]',
   primary key (GroupID, PrivilegeID)
);

alter table admin_group_privilege comment '用户组权限';

/*==============================================================*/
/* Table: admin_menu                                            */
/*==============================================================*/
create table admin_menu
(
   MenuID               bigint not null auto_increment  comment '菜单编码',
   Title                varchar(50)  comment '菜单显示标题',
   Kind                 tinyint not null default 0  comment '类型 0-目录项 1-链接项',
   Icon                 varchar(250)  comment '图标',
   Url                  varchar(250)  comment '菜单URL，尽量使用相对路径',
   UrlTarget            tinyint not null default 0  comment '链接模式 0-TAB打开 1-新窗口打开',
   PrivParams           text  comment '页面内可分配的权限列表，包含功能和数据。json格式：类型-参数
             如：[{Type:ControlID,Params:btnOk},{Type:ControlID,Params:btnOk}]',
   Pinyin               varchar(20)  comment '拼音',
   Note                 varchar(500)  comment '描述',
   OrderNum             int not null default 0  comment '排序，从小到大',
   ParentID             bigint not null default 0  comment '父菜单编码 0-根菜单',
   Status               tinyint not null default 0  comment '状态 0-无效 1-有效',
   SiteID               varchar(50) not null  comment '站点编码',
   primary key (MenuID)
);

alter table admin_menu comment '管理菜单';

/*==============================================================*/
/* Table: admin_privilege                                       */
/*==============================================================*/
create table admin_privilege
(
   PrivilegeID          bigint not null auto_increment  comment '权限ID',
   PrivilegeName        varchar(100)  comment '权限名称',
   Kind                 tinyint not null default 0  comment '权限类型 0-自定义 1-菜单表Menu',
   KindId               varchar(255)  comment '权限类型标识，如来源表编码',
   Note                 varchar(255)  comment '描述',
   Status               tinyint not null default 0  comment '状态 0-无效 1-有效',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '记录日期',
   primary key (PrivilegeID)
);

alter table admin_privilege comment '权限';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create unique index index_1 on admin_privilege
(
   Kind,
   KindId
);

/*==============================================================*/
/* Table: admin_site                                            */
/*==============================================================*/
create table admin_site
(
   SiteID               varchar(50) not null  comment '站点编码',
   SiteName             varchar(200)  comment '站点名称',
   AuthMode             tinyint not null  comment '认证模式
             0-未知
             1-映射授权用户
             2-IdentityServer4',
   Secrets              varchar(200)  comment '站点秘钥',
   Note                 varchar(500)  comment '描述',
   Status               tinyint not null default 0  comment '状态 0-无效 1-有效',
   primary key (SiteID)
);

alter table admin_site comment '应用后台表';

/*==============================================================*/
/* Table: admin_site_mapuser                                    */
/*==============================================================*/
create table admin_site_mapuser
(
   AdminID              bigint not null  comment '用户ID',
   SiteID               varchar(50) not null  comment '站点编码',
   MapUserId            varchar(50)  comment '映射用户编码',
   primary key (AdminID, SiteID)
);

alter table admin_site_mapuser comment '当前用户映射的站点用户，用于授权';

/*==============================================================*/
/* Table: admin_user                                            */
/*==============================================================*/
create table admin_user
(
   AdminID              bigint not null auto_increment  comment '管理用户ID',
   Username             varchar(50)  comment '登录账户',
   Password             varchar(100)  comment '登录密码',
   RealName             varchar(20)  comment '用户真实姓名',
   Status               tinyint not null default 0  comment '状态 0-无效 1-有效',
   IsAdmin              bool not null default 0  comment '是否管理员',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '记录日期',
   primary key (AdminID)
);

alter table admin_user comment '后台用户表';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create unique index index_1 on admin_user
(
   Username
);

/*==============================================================*/
/* Table: admin_user_group                                      */
/*==============================================================*/
create table admin_user_group
(
   AdminID              bigint not null  comment '用户ID',
   GroupID              bigint not null  comment '用户组ID',
   primary key (GroupID, AdminID)
);

alter table admin_user_group comment '用户与用户组';

/*==============================================================*/
/* Table: admin_user_privilege                                  */
/*==============================================================*/
create table admin_user_privilege
(
   AdminID              bigint not null  comment '用户ID',
   PrivilegeID          bigint not null  comment '权限ID',
   PrivParamsValue      varchar(250)  comment '功能和数据权限参数。包含表明有此权限
             类型-参数-是否有权限| 类型-参数-是否有权限
             如：ControlID-btnOk-true|ControlID-btnCancle-false',
   primary key (PrivilegeID, AdminID)
);

alter table admin_user_privilege comment '用户权限表';

/*==============================================================*/
/* Table: demo_class                                            */
/*==============================================================*/
create table demo_class
(
   ClassID              varchar(10) not null  comment '类别编码',
   Name                 varchar(10)  comment '类别',
   Sort1                int not null  comment '',
   Sort2                int not null  comment '',
   primary key (ClassID)
);

alter table demo_class comment '类别
这里有很多说明';

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
   Year                 year(4) not null  comment '学年',
   CourseID             char(36) not null  comment '课程编码（GUID）',
   Name                 varchar(10)  comment '名称',
   OrderNum             int not null  comment '',
   primary key (Year, CourseID)
);

alter table demo_course comment '课程';

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
   UserID               bigint not null auto_increment  comment '用户编码（自增字段）',
   ClassID              varchar(10)  comment '类别编码',
   FBit                 bit not null default 1  comment '字段1
                     多行1
                     多行2',
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

alter table demo_user comment '用户表';

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
   UserID               bigint not null  comment '用户编码（自增字段）',
   Year                 year(4) not null  comment '学年',
   CourseID             char(36) not null  comment '课程编码（GUID）',
   Note                 text  comment '说明',
   primary key (UserID, CourseID, Year)
);

alter table demo_user_course comment '用户分类表';

/*==============================================================*/
/* Table: sys_china_area                                        */
/*==============================================================*/
create table sys_china_area
(
   AreaID               int not null  comment '行政区划码',
   Name                 varchar(20)  comment '省市名称，如北京市',
   ParentId             int not null  comment '父级编码
             0- 根
             100000 -中国',
   ShortName            varchar(20)  comment '名称简写，如北京',
   Level                int not null  comment '级别
             0-根
             1-省，自治区，直辖市，特别行政区
             2-市，地区，自治州，盟，直辖市所属市辖区和县
             3-县，市辖区，县级市，旗
             4-乡镇（街道办事处）',
   CityCode             varchar(10)  comment '城市代码（电话区号）',
   ZipCode              varchar(10)  comment '邮政编码',
   MergerName           varchar(50)  comment '长名称，如中国，北京，北京市',
   Lng                  double(10,5) not null  comment '经度',
   Lat                  double(10,5) not null  comment '纬度',
   PinYin               varchar(20)  comment '拼音',
   JianPin              varchar(20)  comment '简拼',
   Alias                varchar(6)  comment '别名，如川',
   OtherAlias           varchar(10)  comment '其他别名，如蜀',
   Status               int not null default 0  comment '有效状态
             0-有效
             1-无效
             2-变更
             3-删除',
   primary key (AreaID)
);

alter table sys_china_area comment '中国省市4级数据';

/*==============================================================*/
/* Table: sys_log                                               */
/*==============================================================*/
create table sys_log
(
   LogID                bigint not null auto_increment  comment '主键',
   Timestamp            datetime not null  comment '日志发生时间',
   Level                varchar(15)  comment '日志级别
             Verbose|Debug|Information|Warning|Error|Fatal',
   Template             text  comment '消息模板',
   Message              text  comment '消息内容',
   Exception            text  comment '异常',
   Properties           text  comment '属性',
   RecDate              datetime not null default CURRENT_TIMESTAMP  comment '记录时间',
   ProjectId            varchar(200)  comment '项目编码',
   MachineIP            varchar(15)  comment '服务器IP',
   TemplateHash         bigint  comment '消息模板hash',
   primary key (LogID)
);

alter table sys_log comment '系统日志';

/*==============================================================*/
/* Index: index_1                                               */
/*==============================================================*/
create index index_1 on sys_log
(
   Timestamp
);

/*==============================================================*/
/* View: v_admin_priv                                           */
/*==============================================================*/
create VIEW  v_admin_priv
 as
select
   t1.PrivilegeID,
   t1.PrivilegeName,
   t1.Kind as PrivKind,
   t2.MenuID,
   t2.Title as MenuTitle,
   t2.Icon as MenuIcon,
   t2.Kind as MenuKind,
   t2.Url as MenuUrl,
   t2.UrlTarget as MenuUrlTarget,
   t2.PrivParams as MenuPrivParams,
   t2.OrderNum as MenuOrderNum,
   t2.ParentID as MenuParentID,
   t2.Pinyin as MenuPinyin,
   t2.Note as MenuNote,
   t1.Note as PrivNote
from
   admin_privilege t1
   left join admin_menu t2 on  t1.Kind = 1 and t1.KindId = t2.MenuID and t2.Status = 1
where
   t1.Status = 1;

/*==============================================================*/
/* View: v_admin_user_group                                     */
/*==============================================================*/
create VIEW  v_admin_user_group
 as
select t2.AdminID, t2.RealName, t3.GroupID, t3.GroupName
from admin_user_group t1 
	left join admin_user t2 on t2.Status=1 and t1.AdminID = t2.AdminID
	left join admin_group t3 on t3.Status=1 and t1.GroupID=t3.GroupID;

/*==============================================================*/
/* View: v_admin_user_priv                                      */
/*==============================================================*/
create VIEW  v_admin_user_priv
 as
select
   t1.AdminID,
   null as GroupID,
   t1.PrivParamsValue,
   t1.MapUserID,
   t2.*
from
   admin_user_privilege t1
   left join v_admin_priv t2 on  t1.PrivilegeID = t2.PrivilegeID
group by
   PrivilegeID

UNION
select
   t2.AdminID,
   t1.GroupID,
   t1.PrivParamsValue,
   t1.MapUserID,
   t3.*
from
   admin_group_privilege t1
   left join v_admin_user_group t2 on  t1.GroupID = t2.GroupID
   left join v_admin_priv t3 on  t1.PrivilegeID = t3.PrivilegeID;

/*==============================================================*/
/* View: v_demo_user_course                                     */
/*==============================================================*/
create VIEW  v_demo_user_course
 as
select t2.UserID, t2.ClassID, t3.CourseID, t3.Name, t1.Note, '测试列' as TestColumn
from demo_user_course as t1
	left join demo_user as t2 on t1.UserID = t2.UserID
	left join demo_course as t3 on t1.CourseID = t3.CourseID;

alter table demo_user add constraint FK_DEMO_USE_REFERENCE_DEMO_CLA foreign key (ClassID)
      references demo_class (ClassID) on delete restrict on update restrict;

alter table demo_user_course add constraint FK_DEMO_USE_REFERENCE_DEMO_USE foreign key (UserID)
      references demo_user (UserID) on delete restrict on update restrict;

alter table demo_user_course add constraint FK_DEMO_USE_REFERENCE_DEMO_COU foreign key (Year, CourseID)
      references demo_course (Year, CourseID) on delete restrict on update restrict;


create procedure p_demo_get_user_course(in pUserID bigint,out pPageCount int)
comment '存储过程描述'
begin
	select count(0) into pPageCount from demo_user_course where UserID = pUserID;
	select * from demo_user_course where UserID = pUserID;
	select 'abc';
	-- return 123
end;

