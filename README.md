# lp

一个基于 **Unity** 开发的 **2D 像素风剧情 / 调查 / 解谜 Demo 项目**。  
本项目当前处于游戏框架搭建与功能整合阶段，重点在于完成基础系统联动，并逐步补充剧情内容、场景表现与玩法细节。

---

## 一、项目简介

本项目是一个以 **剧情推进、场景调查、对话交互、任务管理、基础存档** 为核心的 Unity Demo。  
当前开发目标并非一次性完成全部游戏内容，而是优先建立一套可持续扩展、便于维护的基础框架，为后续接入更多场景、剧情、交互对象、推理玩法与美术资源打好基础。

目前项目已经具备以下方向的基础能力：

- 主菜单与暂停菜单
- 场景切换
- 基础音频系统
- 基础对话系统
- 基础任务系统
- 基础存档系统
- 调查交互触发

---

## 二、项目定位

- **项目类型**：2D / 像素风 / 剧情 / 调查 / 解谜 / Demo
- **开发引擎**：Unity
- **当前阶段**：游戏基础框架开发阶段
- **项目目标**：先完成可运行的核心框架，再逐步补充玩法与内容

---

## 三、当前已完成 / 已搭建的模块

### 1. 菜单与界面系统
- 主菜单
- 暂停菜单
- 设置相关基础界面
- 场景切换入口

### 2. 音频系统
- BGM 与 SFX 基础管理
- 音量调节控制
- 按钮音效响应

### 3. 对话系统
- 基础对话数据结构
- 对话管理器
- 对话触发逻辑

### 4. 任务系统
- 基础任务数据结构
- 任务管理器
- 任务 UI 显示控制

### 5. 存档系统
- 基础存档数据结构
- 存档管理器
- 存档槽 UI
- 玩家读档恢复逻辑

### 6. 调查交互系统
- 场景交互触发
- 对话交互触发入口

---

## 四、项目目录结构

```text
Assets
├─ Animations
├─ Art
├─ Audio
├─ Data
├─ Prefabs
├─ Resources
├─ Scenes
├─ Scripts
├─ Settings
└─ StreamingAssets

Packages
ProjectSettings
```

---

## 五、Scripts 目录说明

```text
Assets/Scripts
├─ Core
├─ Deduction
├─ Dialogue
├─ Evidence
├─ Interaction
├─ Investigation
├─ Managers
├─ Player
├─ Task
├─ UI
└─ Utils
```

### 主要目录职责

- **Core**：存放公共基础数据、共享结构和通用类型
- **Deduction**：存放推理 / 演绎相关逻辑
- **Dialogue**：存放对话相关的数据与逻辑
- **Evidence**：存放证物、线索相关逻辑
- **Interaction**：存放通用交互逻辑
- **Investigation**：存放调查场景中的交互与流程逻辑
- **Managers**：存放全局管理器脚本
- **Player**：存放玩家相关逻辑，例如读档恢复、状态同步等
- **Task**：存放任务系统相关逻辑
- **UI**：存放菜单、面板、按钮、列表等界面控制脚本
- **Utils**：存放通用工具类

---

## 六、核心脚本说明

### 数据类
- **GameTaskData**：任务数据结构，用于保存任务相关信息
- **DialogueLine**：对话数据结构，用于描述单条对话内容
- **SaveData**：存档数据结构，用于保存游戏进度相关数据

### 全局管理器
- **AudioManager**：全局音频管理，负责 BGM / SFX 播放及音量控制
- **DialogueManager**：全局对话管理，负责对话流程控制
- **GameManager**：全局游戏流程管理
- **SaveManager**：存档管理，负责保存、读取等流程
- **TaskManager**：任务系统管理，负责任务状态维护
- **PauseManagerrScript**：暂停状态管理  
  > 注：后续建议统一命名为 `PauseManager` 或 `PauseStateManager`

### UI 控制脚本
- **MainMenuController**：主菜单界面控制
- **PauseMenuController**：暂停菜单界面控制
- **SaveSlotUI**：存档槽 UI 控制
- **TaskUIController**：任务面板 UI 控制
- **UIButtonSound**：按钮音效控制
- **AudioController**：音频设置界面控制
- **AudioSettingsBinder**：音频设置项与系统绑定
- **SceneLoader**：场景加载入口
- **MainMenuGameEntry**：主菜单进入游戏流程控制

### 交互 / 玩家相关脚本
- **InteractionDialogueTrigger**：交互触发对话
- **PlayerSaveLoader**：玩家读档后状态 / 位置恢复

---

## 七、场景结构

项目当前已包含或已预留以下场景目录：

- `Scenes/MainMenu`
- `Scenes/Investigation`
- `Scenes/Deduction`
- `Scenes/Demo`
- `Scenes/Test`

### 场景职责说明

- **MainMenu**：主菜单场景
- **Investigation**：调查玩法相关场景
- **Deduction**：推理 / 演绎相关场景
- **Demo**：当前主要测试与整合场景
- **Test**：功能测试场景

---

## 八、运行方式

1. 使用 **Unity Hub** 打开本项目
2. 等待项目资源导入完成
3. 打开主菜单场景或当前测试场景
4. 点击运行进行测试

> 如运行后出现引用丢失、按钮无响应、面板不显示等问题，请优先检查：
> - Inspector 中的脚本挂载
> - 对象引用是否绑定完整
> - Button 的 OnClick 事件是否正确配置
> - 场景对象是否启用

---

## 九、当前开发重点

当前阶段的开发重点包括：

1. 完善整体游戏框架
2. 稳定主菜单、暂停菜单、设置菜单之间的联动
3. 稳定存档与读档流程
4. 完善任务系统与调查交互之间的关联
5. 为后续剧情与场景扩展预留结构

---

## 十、后续开发计划

### 近期计划
- 完善主菜单与暂停菜单逻辑
- 优化存档与读档的稳定性
- 完善任务系统的数据与 UI 刷新逻辑
- 梳理调查交互与对话系统的触发关系
- 补充 README、模块说明与开发文档

### 中期计划
- 增加更多调查点与可交互对象
- 完善证物 / 线索系统
- 增加剧情流程控制
- 优化场景切换体验
- 优化 UI 风格与交互体验

### 后期计划
- 加入更完整的推理 / 演绎玩法
- 形成较完整的 Demo 流程
- 完成主要场景串联
- 优化存档、任务、对话之间的数据协同
- 进一步提升项目结构清晰度与可维护性

---

## 十一、开发规范

为保证项目后续扩展和协作效率，当前约定以下开发规范：

### 目录规范
- 管理器脚本统一放在 `Assets/Scripts/Managers`
- UI 控制脚本统一放在 `Assets/Scripts/UI`
- 玩家相关逻辑统一放在 `Assets/Scripts/Player`
- 调查 / 交互逻辑优先放在 `Assets/Scripts/Investigation` 或 `Assets/Scripts/Interaction`
- 数据结构类优先按模块放入 `Core`、`Dialogue`、`Task` 等目录

### 命名规范
- 管理器类尽量使用 `XXXManager`
- 界面控制类尽量使用 `XXXController`
- 数据结构类尽量使用 `XXXData`
- 加载类尽量使用 `XXXLoader`
- 触发类尽量使用 `XXXTrigger`

### 开发约定
- 尽量采用**最小改动原则**，避免无必要的大范围重构
- 修改脚本时，尽量保持已有公开字段与 Inspector 绑定稳定
- 重要功能模块应优先保证可运行，再进行优化
- 功能完成后，及时进行场景内实际测试
- 提交到 GitHub 前，尽量确认当前版本可正常打开与运行

### 版本管理规范
- `main` 分支用于保存相对稳定版本
- 新功能建议使用独立分支开发，例如：
  - `feature/save-system`
  - `feature/task-ui`
  - `fix/pause-menu`
- 每次提交应尽量写清楚本次改动目的

### Git 忽略规范
以下 Unity 自动生成内容不应纳入正常版本管理：
- `Library`
- `Temp`
- `Logs`
- `UserSettings`

---

## 十二、当前说明

本项目目前仍处于基础框架搭建与模块整合阶段。  
当前版本更偏向**技术验证与结构搭建**，后续将逐步补充：

- 更完整的 README
- 模块职责说明文档
- 场景说明文档
- 存档系统说明
- 任务系统说明
- 交互与剧情流程说明

---

## 十三、备注

- 当前仓库主要用于项目版本管理与结构整理
- 后续将逐步提高代码命名规范化程度
- 若脚本名称与职责存在不一致情况，将在后续整理中逐步修正
